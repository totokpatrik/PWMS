using Castle.DynamicLinqQueryBuilder;
using Microsoft.AspNetCore.Identity;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Core.Sites.Repositories;
using PWMS.Domain.Auth.Entities;
using PWMS.Domain.Core.Sites.Entities;

namespace PWMS.Persistence.PortgreSQL.Core.Sites.Repositories;

public class SiteRepository : RepositoryBase<Site>, ISiteRepository
{
    private readonly IApplicationDbContext _dbContext;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public SiteRepository(IApplicationDbContext dbContext, UserManager<User> userManager, RoleManager<Role> roleManager) : base(dbContext.AppDbContext)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
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
