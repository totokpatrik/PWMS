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
        string? siteId = _dbContext.AppDbContext.Set<Site>().FirstOrDefault()?.Id.ToString();
        claims.Add(new Claim("Site", siteId ?? string.Empty));

        // Warehouse claim
        string? warehouseId = _dbContext.AppDbContext.Set<Warehouse>().FirstOrDefault()?.Id.ToString();
        claims.Add(new Claim("Warehouse", warehouseId ?? string.Empty));

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
