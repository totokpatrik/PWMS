using PWMS.Domain.Core.Warehouses.Entities;

namespace PWMS.Application.Core.Warehouses.Specifications;

public class WarehouseByIdSpecification : Specification<Warehouse>, ISingleResultSpecification<Warehouse>
{
    public WarehouseByIdSpecification(Guid id, string currentUserId, bool noTracking = false)
    {
        Query.Where(i => i.Id == id);
        if (noTracking)
        {
            Query.AsNoTracking();
        }
        Query
            .Where(s => s.Owner.Id == currentUserId || s.Admins.Any(a => a.Id == currentUserId) || s.Users.Any(u => u.Id == currentUserId));
    }
}
