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

        var token = await BuildToken(username, password);

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
        var token = await BuildToken(username, password);

        return token;
    }

    private async Task<Token> BuildToken(string username, string password)
    {
        var jwtConfiguration = _configuration.GetSection(JwtConfigurationSection.SectionName).Get<JwtDetails>();
        ArgumentNullException.ThrowIfNull(jwtConfiguration);

        var claims = new List<Claim>()
            {
                new Claim("username", username)
            };

        var user = await _userManager.FindByNameAsync(username);
        ArgumentNullException.ThrowIfNull(user);
        var claimsDb = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        claims.Add(new Claim("Id", user.Id));

        foreach (var role in roles)
        {
            var claim = new Claim(ClaimTypes.Role, role);
            claims.Add(claim);
        }

        // Site claim
        var siteContext = _dbContext.AppDbContext.Set<Site>();

        string? siteId = siteContext.FirstOrDefault()?.Id.ToString();
        claims.Add(new Claim("Site", siteId ?? string.Empty));

        // Site roles
        var siteOwner = siteContext
            .FirstOrDefault(s => s.Owner == user);

        if (siteOwner != null)
            claims.Add(new Claim(ClaimTypes.Role, PWMS.Application.Common.Identity.Roles.Role.SiteOwner));

        var siteAdmin = siteContext
            .Select(s => s.Admins)
            .Where(a => a.Contains(user))
            .Any();

        if (siteAdmin)
            claims.Add(new Claim(ClaimTypes.Role, PWMS.Application.Common.Identity.Roles.Role.SiteAdmin));

        var siteUser = siteContext
            .Select(s => s.Users)
            .Where(a => a.Contains(user))
            .Any();

        if (siteUser)
            claims.Add(new Claim(ClaimTypes.Role, PWMS.Application.Common.Identity.Roles.Role.SiteUser));

        // Warehouse claim
        var warehouseContext = _dbContext.AppDbContext.Set<Warehouse>();

        string? warehouseId = warehouseContext.FirstOrDefault()?.Id.ToString();
        claims.Add(new Claim("Warehouse", warehouseId ?? string.Empty));

        //Warehouse roles
        var warehouseOwner = warehouseContext
            .FirstOrDefault(s => s.Owner == user);

        if (warehouseOwner != null)
            claims.Add(new Claim(ClaimTypes.Role, PWMS.Application.Common.Identity.Roles.Role.WarehouseOwner));

        var warehouseAdmin = warehouseContext
            .Select(s => s.Admins)
            .Where(a => a.Contains(user))
            .Any();

        if (warehouseAdmin)
            claims.Add(new Claim(ClaimTypes.Role, PWMS.Application.Common.Identity.Roles.Role.WarehouseAdmin));

        var warehouseUser = warehouseContext
            .Select(s => s.Users)
            .Where(a => a.Contains(user))
            .Any();

        if (warehouseUser)
            claims.Add(new Claim(ClaimTypes.Role, PWMS.Application.Common.Identity.Roles.Role.WarehouseUser));

        claims.AddRange(claimsDb);

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
}
