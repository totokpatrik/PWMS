using Castle.DynamicLinqQueryBuilder;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Repositories;
using PWMS.Domain.Configuration.Inventory.Items.Entities;

namespace PWMS.Persistence.PortgreSQL.Configurations.Inventory.Items.FootprintDetails.Repositories;

public class FootprintDetailRepository : RepositoryBase<FootprintDetail>, IFootprintDetailRepository
{
    private readonly IApplicationDbContext _dbContext;

    public FootprintDetailRepository(IApplicationDbContext dbContext) : base(dbContext.AppDbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<FootprintDetail>> GetAllFootprints(ISpecification<FootprintDetail> specification, CancellationToken cancellationToken, QueryBuilderFilterRule filter)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: _dbContext.Set<FootprintDetail>().AsQueryable().BuildQuery(filter),
            specification: specification);

        return await queryResult.ToListAsync(cancellationToken);
    }
}
