using PWMS.Domain.Configuration.Inventory.Items.Entities;

namespace PWMS.Application.Configurations.Inventory.Items.Footprints.Specifications;

public sealed class FootprintExceptIdSpecification : Specification<Footprint>
{
    public FootprintExceptIdSpecification(Guid id, bool noTracking = false)
    {
        Query.Where(i => i.Id != id);
        if (noTracking)
        {
            Query.AsNoTracking();
        }
    }
}
