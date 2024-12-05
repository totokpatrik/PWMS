using PWMS.Domain.Inventories.Items.Entities;

namespace PWMS.Application.Configuration.Inventory.Items.ItemFamilies.Specifications;

public sealed class ItemFamilyByIdSpecification : Specification<ItemFamily>, ISingleResultSpecification<ItemFamily>
{
    public ItemFamilyByIdSpecification(Guid id, bool noTracking = false)
    {
        Query.Where(i => i.Id == id);
        if (noTracking)
        {
            Query.AsNoTracking();
        }
    }
}
