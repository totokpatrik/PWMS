using Castle.DynamicLinqQueryBuilder;
using PWMS.Domain.Configuration.Inventory.Items.Entities;

namespace PWMS.Application.Configurations.Inventory.Items.Items.Repositories;

public interface IItemRepository : IRepositoryBase<Item>
{
    Task<List<Item>> GetAllItems(ISpecification<Item> specification, CancellationToken cancellationToken, QueryBuilderFilterRule filter);
}
