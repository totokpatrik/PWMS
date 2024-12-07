using Castle.DynamicLinqQueryBuilder;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Repositories;
using PWMS.Domain.Configuration.Inventory.Items.Entities;

namespace PWMS.Persistence.PortgreSQL.Inventories.Repositories;

public class ItemFamilyGroupRepository : RepositoryBase<ItemFamilyGroup>, IItemFamilyGroupRepository
{
    private readonly IApplicationDbContext _dbContext;

    public ItemFamilyGroupRepository(IApplicationDbContext dbContext) : base(dbContext.AppDbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ItemFamilyGroup>> GetAllItemFamilyGroups(
        ISpecification<ItemFamilyGroup> specification,
        CancellationToken cancellationToken, QueryBuilderFilterRule filter)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: _dbContext.Set<ItemFamilyGroup>().AsQueryable().BuildQuery(filter),
            specification: specification);

        return await queryResult.ToListAsync(cancellationToken);
    }
}
