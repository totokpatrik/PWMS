using Castle.DynamicLinqQueryBuilder;
using PWMS.Domain.Addresses.Entities;
using PWMS.Domain.Core.Sites.Entities;

namespace PWMS.Application.Core.Sites.Repositories;

public interface ISiteRepository : IRepositoryBase<Site>
{
    Task<List<Address>> GetAllSites(ISpecification<Address> specification, CancellationToken cancellationToken, QueryBuilderFilterRule filter);
}