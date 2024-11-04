using PWMS.Application.Addresses.Repositories;
using PWMS.Application.Common.Interfaces;
using PWMS.Domain.Addresses.Entities;
using PWMS.Persistence.PortgreSQL.Addresses.Repositories;

namespace PWMS.Application.Tests.Common;

public class MockAddressRepository : AddressRepository, IAddressRepository
{
    public MockAddressRepository(IApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public override Task<Address> AddAsync(Address entity, CancellationToken cancellationToken = default)
    {
        Thread.Sleep(5000);

        return base.AddAsync(entity, cancellationToken);
    }
}
