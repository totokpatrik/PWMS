using PWMS.Core.SharedKernel;
using PWMS.Domain.Addresses.Aggregates;
using System;

namespace PWMS.Application.Addresses.Repositories;

public interface IAddressRepository : IBaseRepository<Address, Guid>
{
}
