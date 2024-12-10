using PWMS.Domain.Configuration.Inventory.Items.Entities;

namespace PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Specifications;
public sealed class FootprintDetailByIdSpecification : Specification<FootprintDetail>, ISingleResultSpecification<FootprintDetail>
{
    public FootprintDetailByIdSpecification(Guid id, bool noTracking = false)
    {
        Query.Where(i => i.Id == id);
        if (noTracking)
        {
            Query.AsNoTracking();
        }
    }
}
