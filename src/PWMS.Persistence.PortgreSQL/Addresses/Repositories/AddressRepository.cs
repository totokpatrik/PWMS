using PWMS.Application.Addresses.Repositories;
using PWMS.Application.Common.Interfaces;
using PWMS.Domain.Addresses.Entities;

namespace PWMS.Persistence.PortgreSQL.Addresses.Repositories;

public class AddressRepository : RepositoryBase<Address>, IAddressRepository
{
    public AddressRepository(IApplicationDbContext dbContext) : base(dbContext.AppDbContext)
    {
    }
    public AddressRepository(IApplicationDbContext dbContext, ISpecificationEvaluator specificationEvaluator) : base(dbContext.AppDbContext, specificationEvaluator)
    {
    }
}
