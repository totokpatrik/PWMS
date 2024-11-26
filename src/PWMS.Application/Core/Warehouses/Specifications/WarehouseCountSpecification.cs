using PWMS.Domain.Core.Warehouses.Entities;

namespace PWMS.Application.Core.Warehouses.Specifications;

public class WarehouseCountSpecification : Specification<Warehouse>, ISingleResultSpecification<Warehouse>
{
    public WarehouseCountSpecification(string currentUserId, bool noTracking = false)
    {
        Query
            .Where(s => s.Owner.Id == currentUserId || s.Admins.Any(a => a.Id == currentUserId) || s.Users.Any(u => u.Id == currentUserId));
    }
}