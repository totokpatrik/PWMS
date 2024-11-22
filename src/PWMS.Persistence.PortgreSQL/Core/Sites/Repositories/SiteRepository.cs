using Castle.DynamicLinqQueryBuilder;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Core.Sites.Repositories;
using PWMS.Domain.Core.Sites.Entities;

namespace PWMS.Persistence.PortgreSQL.Core.Sites.Repositories;

public class SiteRepository : RepositoryBase<Site>, ISiteRepository
{
    private readonly IApplicationDbContext _dbContext;

    public SiteRepository(IApplicationDbContext dbContext) : base(dbContext.AppDbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Site>> GetAllSites(ISpecification<Site> specification, CancellationToken cancellationToken, QueryBuilderFilterRule filter)
    {
        var queryResult = SpecificationEvaluator.Default.GetQuery(
            query: _dbContext.Set<Site>().AsQueryable().BuildQuery(filter),
            specification: specification)
            .Include(us => us.UsersSelected);

        return await queryResult.ToListAsync(cancellationToken);
    }
}
