using Castle.DynamicLinqQueryBuilder;
using PWMS.Domain.Configuration.Inventory.Items.Entities;

namespace PWMS.Application.Configurations.Inventory.Items.ItemFamilyGroups.Repositories;

public interface IItemFamilyGroupRepository : IRepositoryBase<ItemFamilyGroup>
{
    Task<List<ItemFamilyGroup>> GetAllItemFamilyGroups(ISpecification<ItemFamilyGroup> specification, CancellationToken cancellationToken, QueryBuilderFilterRule filter);
}
