using Castle.DynamicLinqQueryBuilder;
using PWMS.Domain.Addresses.Entities;

namespace PWMS.Application.Addresses.Repositories;

public interface IAddressRepository : IRepositoryBase<Address>
{
    Task<List<Address>> GetAllAddresses(ISpecification<Address> specification, CancellationToken cancellationToken, QueryBuilderFilterRule filter);
}
