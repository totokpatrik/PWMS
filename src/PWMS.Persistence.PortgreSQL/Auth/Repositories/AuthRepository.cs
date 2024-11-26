using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PWMS.Application.Auth.Repositories;
using PWMS.Application.Common.Exceptions;
using PWMS.Application.Common.Interfaces;
using PWMS.Domain.Auth.Entities;
using PWMS.Domain.Core.Sites.Entities;
using PWMS.Domain.Core.Warehouses.Entities;
using PWMS.Persistence.PortgreSQL.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PWMS.Persistence.PortgreSQL.Auth.Repositories;

public class AuthRepository : RepositoryBase<User>, IAuthRepository
{
    private readonly IApplicationDbContext _dbContext;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthRepository(
        IApplicationDbContext dbContext,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IConfiguration configuration) : base(dbContext.AppDbContext)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    public async Task<Token> Login(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user == null)
        {
            throw new UnauthorizedException("Username or password is incorrect.");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: true);

        if (!result.Succeeded)
        {
            throw new UnauthorizedException("Username or password is incorrect.");
        }

        var token = await BuildToken(username);

        return token;
    }

    public async Task<Token> Register(string username, string password)
    {
        User user = new User { UserName = username };
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded && result.Errors.Any())
        {
            throw new RegisterException(result.Errors);
        }
        var token = await BuildToken(username);

        return token;
    }

    public async Task<Token> BuildToken(string username)
    {
        var jwtConfiguration = _configuration.GetSection(JwtConfigurationSection.SectionName).Get<JwtDetails>();
        ArgumentNullException.ThrowIfNull(jwtConfiguration);

        var user = await _userManager.FindByNameAsync(username);
        ArgumentNullException.ThrowIfNull(user);

        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name, username));

        var roles = await _userManager.GetRolesAsync(user);

        // Site claim
        roles.Add(await SiteRole(user.UserName!));
        var selectedSite = await Site(user.UserName!);
        if (selectedSite != null)
        {
            claims.Add(new Claim("SiteId", selectedSite.Id.ToString() ?? string.Empty));
            claims.Add(new Claim("SiteName", selectedSite.Name ?? string.Empty));
        }

        // Warehouse claim
        roles.Add(await WarehouseRole(user.UserName!));
        var selectedWarehouse = await Warehouse(user.UserName!);
        if (selectedWarehouse != null)
        {
            claims.Add(new Claim("WarehouseId", selectedWarehouse.Id.ToString() ?? string.Empty));
            claims.Add(new Claim("WarehouseName", selectedWarehouse.Name ?? string.Empty));
        }
        else
        {
            claims.Add(new Claim("WarehouseId", string.Empty));
            claims.Add(new Claim("WarehouseName", string.Empty));
        }

        string roleString = String.Join(",", roles);
        claims.Add(new Claim(ClaimTypes.Role, roleString));

        var claimsDb = await _userManager.GetClaimsAsync(user);
        claims.AddRange(claimsDb);

        claims.Add(new Claim("Id", user.Id));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Secret));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiration = DateTime.Now.AddHours(3);

        var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration, signingCredentials: creds);

        return new Token()
        {
            TokenString = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        };
    }

    private async Task<Site?> Site(string userName)
    {
        return (await (_dbContext.Set<User>()).Include(u => u.SelectedSite).FirstOrDefaultAsync(u => u.UserName == userName))!.SelectedSite;
    }

    private async Task<Warehouse?> Warehouse(string userName)
    {
        return (await (_dbContext.Set<User>()).Include(u => u.SelectedWarehouse).FirstOrDefaultAsync(u => u.UserName == userName))!.SelectedWarehouse;
    }

    private async Task<string> SiteRole(string userName)
    {
        var userContext = _dbContext.Set<User>();
        var user = await userContext
            .Include(u => u.SelectedSite)
            .FirstOrDefaultAsync(u => u.UserName == userName) ?? throw new NotFoundException(nameof(User));

        var role = string.Empty;

        if (user.SelectedSite == null)
            return role;

        if (user.SelectedSite.Owner.Id == user.Id)
        {
            role = PWMS.Application.Common.Identity.Roles.Role.SiteOwner;
        }

        if (user.SelectedSite.Admins == null)
            return role;

        if (user.SelectedSite.Admins.Any(a => a.Id.Contains(user.Id)))
        {
            role = PWMS.Application.Common.Identity.Roles.Role.SiteAdmin;
        }

        if (user.SelectedSite.Users == null)
            return role;

        if (user.SelectedSite.Users.Any(a => a.Id.Contains(user.Id)))
        {
            role = PWMS.Application.Common.Identity.Roles.Role.SiteUser;
        }

        return role;
    }

    private async Task<string> WarehouseRole(string userName)
    {
        var userContext = _dbContext.Set<User>();
        var user = await userContext
            .Include(u => u.SelectedWarehouse)
            .FirstOrDefaultAsync(u => u.UserName == userName) ?? throw new NotFoundException(nameof(User));

        var role = string.Empty;

        if (user.SelectedWarehouse == null)
            return role;

        if (user.SelectedWarehouse.Owner.Id == user.Id)
        {
            role = PWMS.Application.Common.Identity.Roles.Role.WarehouseOwner;
        }

        if (user.SelectedWarehouse.Admins == null)
            return role;

        if (user.SelectedWarehouse.Admins.Any(a => a.Id.Contains(user.Id)))
        {
            role = PWMS.Application.Common.Identity.Roles.Role.WarehouseAdmin;
        }

        if (user.SelectedWarehouse.Users == null)
            return role;

        if (user.SelectedWarehouse.Users.Any(a => a.Id.Contains(user.Id)))
        {
            role = PWMS.Application.Common.Identity.Roles.Role.WarehouseUser;
        }

        return role;
    }
}
