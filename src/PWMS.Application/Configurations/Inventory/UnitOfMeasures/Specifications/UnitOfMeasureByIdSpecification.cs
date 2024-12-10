using PWMS.Domain.Inventories.Entities;

namespace PWMS.Application.Configurations.Inventory.UnitOfMeasures.Specifications;

public sealed class UnitOfMeasureByIdSpecification : Specification<UnitOfMeasure>, ISingleResultSpecification<UnitOfMeasure>
{
    public UnitOfMeasureByIdSpecification(Guid id, bool noTracking = false)
    {
        Query.Where(i => i.Id == id);
        if (noTracking)
        {
            Query.AsNoTracking();
        }
    }
}
