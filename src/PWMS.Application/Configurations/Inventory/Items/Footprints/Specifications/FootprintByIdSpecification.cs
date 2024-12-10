using PWMS.Domain.Configuration.Inventory.Items.Entities;

namespace PWMS.Application.Configurations.Inventory.Items.Footprints.Specifications;

public sealed class FootprintByIdSpecification : Specification<Footprint>, ISingleResultSpecification<Footprint>
{
    public FootprintByIdSpecification(Guid id, bool noTracking = false)
    {
        Query.Where(i => i.Id == id);
        if (noTracking)
        {
            Query.AsNoTracking();
        }
    }
}
