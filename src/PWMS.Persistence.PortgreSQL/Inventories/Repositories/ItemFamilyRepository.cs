using Castle.DynamicLinqQueryBuilder;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Repositories;
using PWMS.Domain.Configuration.Inventory.Items.Entities;

namespace PWMS.Persistence.PortgreSQL.Inventories.Repositories;

public class ItemFamilyRepository : RepositoryBase<ItemFamily>, IItemFamilyRepository
{
    private readonly IApplicationDbContext _dbContext;

    public ItemFamilyRepository(IApplicationDbContext dbContext) : base(dbContext.AppDbContext)
    {
        _dbContext = dbContext;
    }
    public Task<List<ItemFamily>> GetAllItemFamilies(ISpecification<ItemFamily> specification, CancellationToken cancellationToken, QueryBuilderFilterRule filter)
    {
        throw new NotImplementedException();
    }
}
