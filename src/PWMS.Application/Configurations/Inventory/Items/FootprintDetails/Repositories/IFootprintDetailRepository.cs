using Castle.DynamicLinqQueryBuilder;
using PWMS.Domain.Configuration.Inventory.Items.Entities;

namespace PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Repositories;
public interface IFootprintDetailRepository : IRepositoryBase<FootprintDetail>
{
    Task<List<FootprintDetail>> GetAllFootprints(
        ISpecification<FootprintDetail> specification,
        CancellationToken cancellationToken,
        QueryBuilderFilterRule filter);
}
