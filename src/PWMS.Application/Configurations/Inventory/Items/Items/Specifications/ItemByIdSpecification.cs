using PWMS.Domain.Configuration.Inventory.Items.Entities;

namespace PWMS.Application.Configurations.Inventory.Items.Items.Specifications;

public sealed class ItemByIdSpecification : Specification<Item>, ISingleResultSpecification<Item>
{
    public ItemByIdSpecification(Guid id, bool noTracking = false)
    {
        Query.Where(i => i.Id == id);
        if (noTracking)
        {
            Query.AsNoTracking();
        }
    }
}
