using PWMS.Application.Addresses.Repositories;
using PWMS.Domain.Addresses.Aggregates;
using PWMS.Infrastructure.Data.Context;
using PWMS.Infrastructure.Data.Repositories.Common;
using System;

namespace PWMS.Infrastructure.Data.Repositories.Addresses;

internal class AddressRepository(ApplicationDbContext context)
    : BaseRepository<Address, Guid>(context), IAddressRepository
{
}
