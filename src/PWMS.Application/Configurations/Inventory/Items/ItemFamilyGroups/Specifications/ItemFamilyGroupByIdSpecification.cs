using PWMS.Domain.Configuration.Inventory.Items.Entities;

namespace PWMS.Application.Configurations.Inventory.Items.ItemFamilyGroups.Specifications;

public sealed class ItemFamilyGroupByIdSpecification : Specification<ItemFamilyGroup>, ISingleResultSpecification<ItemFamilyGroup>
{
    public ItemFamilyGroupByIdSpecification(Guid id, bool noTracking = false)
    {
        Query.Where(i => i.Id == id);
        if (noTracking)
        {
            Query.AsNoTracking();
        }
    }
}
