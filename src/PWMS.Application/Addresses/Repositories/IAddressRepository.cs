using Ardalis.Specification;
using PWMS.Domain.Addresses.Entities;

namespace PWMS.Application.Addresses.Repositories;

public interface IAddressRepository : IRepositoryBase<Address>
{
    IQueryable<Address> GetAll(bool noTracking = true);
}
