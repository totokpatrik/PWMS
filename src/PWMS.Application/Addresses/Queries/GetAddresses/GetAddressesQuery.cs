using PWMS.Application.Common.CQRS;
using PWMS.Application.Common.Paging;
using PWMS.Application.Models;

namespace PWMS.Application.Addresses.Queries.GetAddresses;

public record GetAddressesQuery(PaginationRequest PaginationRequest)
    : IQuery<GetAddressesResult>;

public record GetAddressesResult(PaginatedResult<AddressDto> Addresses);