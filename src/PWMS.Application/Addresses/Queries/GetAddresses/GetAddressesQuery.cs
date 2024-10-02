using PWMS.Application.Abstractions.Paging;
using PWMS.Application.Abstractions.Queries;
using PWMS.Application.Addresses.Models;

namespace PWMS.Application.Addresses.Queries.GetAddresses;

public record GetAddressesQuery(PaginationRequest PaginationRequest)
    : Query<PaginatedResult<AddressDto>>;