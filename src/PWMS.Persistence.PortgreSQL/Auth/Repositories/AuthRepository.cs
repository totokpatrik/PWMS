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

        var roles = await _userManager.GetRolesAsync(user);
        string roleString = String.Join(",", roles);

        claims.Add(new Claim(ClaimTypes.Name, username));
        claims.Add(new Claim(ClaimTypes.Role, roleString));

        // Site claim
        var siteContext = _dbContext.AppDbContext.Set<Site>();
        var userContext = _dbContext.AppDbContext.Set<User>();

        var selectedSite = (await userContext
            .Include(u => u.SelectedSite)
            .FirstOrDefaultAsync(u => u.Id == user.Id)).SelectedSite;

        claims.Add(new Claim("SiteId", selectedSite?.Id.ToString() ?? string.Empty));
        claims.Add(new Claim("SiteName", selectedSite?.Name ?? string.Empty));


        // Warehouse claim
        var warehouseContext = _dbContext.AppDbContext.Set<Warehouse>();

        var selectedWarehouse = (await userContext
            .Include(u => u.SelectedWarehouse)
            .FirstOrDefaultAsync(u => u.Id == user.Id)).SelectedWarehouse;

        claims.Add(new Claim("WarehouseId", selectedWarehouse?.Id.ToString() ?? string.Empty));
        claims.Add(new Claim("WarehouseName", selectedWarehouse?.Name ?? string.Empty));

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

    public async Task SelectSite(Site site, string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user == null)
        {
            throw new NotFoundException(nameof(User));
        }

        if (site.Owner.Id == user.Id)
        {
            await _userManager.AddToRoleAsync(user, PWMS.Application.Common.Identity.Roles.Role.SiteOwner);
        }

        if (site.Admins.Any(a => a.Id.Contains(user.Id)))
        {
            await _userManager.AddToRoleAsync(user, PWMS.Application.Common.Identity.Roles.Role.SiteAdmin);
        }

        if (site.Users.Any(a => a.Id.Contains(user.Id)))
        {
            await _userManager.AddToRoleAsync(user, PWMS.Application.Common.Identity.Roles.Role.SiteUser);
        }
    }

    public async Task SelectWarehouse(Warehouse warehouse, string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user == null)
        {
            throw new NotFoundException(nameof(User));
        }

        if (warehouse.Owner == user)
        {
            await _userManager.AddToRoleAsync(user, PWMS.Application.Common.Identity.Roles.Role.WarehouseOwner);
        }

        if (warehouse.Admins.Contains(user))
        {
            await _userManager.AddToRoleAsync(user, PWMS.Application.Common.Identity.Roles.Role.WarehouseAdmin);
        }

        if (warehouse.Users.Contains(user))
        {
            await _userManager.AddToRoleAsync(user, PWMS.Application.Common.Identity.Roles.Role.WarehouseUser);
        }
    }
}
