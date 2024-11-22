using Microsoft.AspNetCore.Http;
using PWMS.Common.Extensions;

namespace PWMS.Infrastructure.Core.Services;

public class CurrentSIteService(IHttpContextAccessor httpContextAccessor) : ICurrentSiteService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor.ThrowIfNull();

    public ICurrentSite? GetCurrentSite()
    {
        if (_httpContextAccessor.HttpContext is null || !_httpContextAccessor.HttpContext.User.Identity!.IsAuthenticated)
        {
            return null;
        }

        var warehouseId = GetSingleClaimValue("Warehouse");

        if (string.IsNullOrWhiteSpace(warehouseId))
        {
            return new CurrentSite
            {
                Id = Guid.Empty
            };
        }

        return new CurrentSite
        {
            Id = Guid.Parse(warehouseId)
        };
    }
    private string GetSingleClaimValue(string claimType) =>
    _httpContextAccessor.HttpContext!.User.Claims
        .Single(claim => claim.Type == claimType)
        .Value;
}