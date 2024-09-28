using PWMS.Application.Common.Paging;
using PWMS.Application.Models;
using PWMS.Domain.Models;
using PWMS.Domain.Models.ValueObjects;

namespace PWMS.Application.Common.Interfaces;

public interface IAddressRepository
{
    Task AddAsync(Address address, CancellationToken cancellationToken);
    Task<PaginatedResult<AddressDto>> GetAsync(PaginationRequest paginationRequest, CancellationToken cancellationToken);
    Task<Address?> GetByIdAsync(AddressId addressId, CancellationToken cancellationToken);
    Task RemoveAsync(Address address, CancellationToken cancellationToken);
    Task<Address> UpdateAsync(Address address, CancellationToken cancellationToken);
}
