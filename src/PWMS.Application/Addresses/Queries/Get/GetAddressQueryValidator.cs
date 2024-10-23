using PWMS.Application.Addresses.Models;
using PWMS.Application.Common.Paging;

namespace PWMS.Application.Addresses.Queries.Get;

internal sealed class GetAddressQueryValidator
    : PagingQueryValidator<GetAddressQuery, Result<CollectionViewModel<AddressDto>>>
{
}
