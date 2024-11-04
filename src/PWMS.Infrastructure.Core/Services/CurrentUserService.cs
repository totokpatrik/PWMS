using Microsoft.AspNetCore.Http;
using PWMS.Common.Extensions;

namespace PWMS.Infrastructure.Core.Services;

public sealed class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor.ThrowIfNull();

    public ICurrentUser? GetCurrentUser()
    {

        if (_httpContextAccessor.HttpContext is null || !_httpContextAccessor.HttpContext.User.Identity!.IsAuthenticated)
        {
            return null;
        }

        var username = GetSingleClaimValue("username");
        var id = GetSingleClaimValue("Id");
        /*var permissions = GetClaimValues("permissions");
        var roles = GetClaimValues(ClaimTypes.Role);
        var firstName = GetSingleClaimValue(JwtRegisteredClaimNames.Name);
        var lastName = GetSingleClaimValue(ClaimTypes.Surname);
        var email = GetSingleClaimValue(ClaimTypes.Email);
        */
        return new CurrentUser
        {
            Id = id,
            Username = username
        };
    }

    /*
    private List<string> GetClaimValues(string claimType) =>
        _httpContextAccessor.HttpContext!.User.Claims
            .Where(claim => claim.Type == claimType)
            .Select(claim => claim.Value)
            .ToList();
    */
    private string GetSingleClaimValue(string claimType) =>
        _httpContextAccessor.HttpContext!.User.Claims
            .Single(claim => claim.Type == claimType)
            .Value;
}
