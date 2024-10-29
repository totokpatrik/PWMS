using PWMS.Application.Common.Interfaces;
using PWMS.Domain.Addresses.Entities;
using PWMS.Persistence.PortgreSQL.Addresses.Repositories;

namespace PWMS.Presentation.Rest.Tests.Common.InternalError;

public class MockAddressRepositoryInternalError : AddressRepository
{
    public MockAddressRepositoryInternalError(IApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public override Task<Address> AddAsync(Address entity, CancellationToken cancellationToken = default)
    {
        throw new Exception();
    }
}
