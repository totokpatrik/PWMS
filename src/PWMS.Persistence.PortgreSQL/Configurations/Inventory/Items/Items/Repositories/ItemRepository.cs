using Castle.DynamicLinqQueryBuilder;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Configurations.Inventory.Items.Items.Repositories;
using PWMS.Domain.Configuration.Inventory.Items.Entities;

namespace PWMS.Persistence.PortgreSQL.Configurations.Inventory.Items.Items.Repositories;

internal class ItemRepository : RepositoryBase<Item>, IItemRepository
{
    private readonly IApplicationDbContext _dbContext;

    public ItemRepository(IApplicationDbContext dbContext) : base(dbContext.AppDbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Item>> GetAllItems(ISpecification<Item> specification, CancellationToken cancellationToken, QueryBuilderFilterRule filter)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: _dbContext.Set<Item>().AsQueryable().BuildQuery(filter),
            specification: specification);

        return await queryResult.ToListAsync(cancellationToken);
    }
}
