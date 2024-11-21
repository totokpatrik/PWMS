using Microsoft.AspNetCore.Http;
using PWMS.Common.Extensions;

namespace PWMS.Infrastructure.Core.Services;

public class CurrentWarehouseService(IHttpContextAccessor httpContextAccessor) : ICurrentWarehouseService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor.ThrowIfNull();

    public ICurrentWarehouse? GetCurrentWarehouse()
    {
        if (_httpContextAccessor.HttpContext is null || !_httpContextAccessor.HttpContext.User.Identity!.IsAuthenticated)
        {
            return null;
        }

        var warehouseId = GetSingleClaimValue("Warehouse");

        if (string.IsNullOrWhiteSpace(warehouseId))
        {
            return new CurrentWarehouse
            {
                Id = Guid.Empty
            };
        }

        return new CurrentWarehouse
        {
            Id = Guid.Parse(warehouseId)
        };
    }
    private string GetSingleClaimValue(string claimType) =>
    _httpContextAccessor.HttpContext!.User.Claims
        .Single(claim => claim.Type == claimType)
        .Value;
}