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

    public Task<List<Item>> GetAllItems(ISpecification<Item> specification, CancellationToken cancellationToken, QueryBuilderFilterRule filter)
    {
        throw new NotImplementedException();
    }
}
