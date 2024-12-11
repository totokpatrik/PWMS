using Castle.DynamicLinqQueryBuilder;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Configurations.Inventory.Items.Footprints.Repositories;
using PWMS.Domain.Configuration.Inventory.Items.Entities;

namespace PWMS.Persistence.PortgreSQL.Configurations.Inventory.Items.Footprints.Repositories;

public class FootprintRepository : RepositoryBase<Footprint>, IFootprintRepository
{
    private readonly IApplicationDbContext _dbContext;

    public FootprintRepository(IApplicationDbContext dbContext) : base(dbContext.AppDbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Footprint>> GetAllFootprints(ISpecification<Footprint> specification, CancellationToken cancellationToken, QueryBuilderFilterRule filter)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: _dbContext.Set<Footprint>().AsQueryable().BuildQuery(filter),
            specification: specification);

        return await queryResult.ToListAsync(cancellationToken);
    }

    public async Task ResetDefaultToFalse()
    {
        var footprints = await ListAsync();

        foreach (var footprint in footprints)
        {
            footprint.Update(footprint.Name, false);
        }

        await SaveChangesAsync();
    }
}
