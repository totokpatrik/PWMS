using Castle.DynamicLinqQueryBuilder;
using PWMS.Domain.Core.Sites.Entities;

namespace PWMS.Application.Core.Sites.Repositories;

public interface ISiteRepository : IRepositoryBase<Site>
{
    Task<List<Site>> GetAllSites(ISpecification<Site> specification, CancellationToken cancellationToken, QueryBuilderFilterRule filter);
}