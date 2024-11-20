using Castle.DynamicLinqQueryBuilder;
using PWMS.Application.Addresses.Repositories;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Core.Sites.Repositories;
using PWMS.Domain.Addresses.Entities;
using PWMS.Domain.Core.Sites.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMS.Persistence.PortgreSQL.Core.Sites.Repositories;

public class SiteRepository : RepositoryBase<Site>, ISiteRepository
{
    private readonly IApplicationDbContext _dbContext;

    public SiteRepository(IApplicationDbContext dbContext) : base(dbContext.AppDbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<Address>> GetAllSites(ISpecification<Address> specification, CancellationToken cancellationToken, QueryBuilderFilterRule filter)
    {
        throw new NotImplementedException();
    }
}
