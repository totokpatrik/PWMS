using Castle.DynamicLinqQueryBuilder;
using PWMS.Domain.Configuration.Inventory.Items.Entities;

namespace PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Repositories;

public interface IItemFamilyRepository : IRepositoryBase<ItemFamily>
{
    Task<List<ItemFamily>> GetAllItemFamilies(ISpecification<ItemFamily> specification, CancellationToken cancellationToken, QueryBuilderFilterRule filter);
}
