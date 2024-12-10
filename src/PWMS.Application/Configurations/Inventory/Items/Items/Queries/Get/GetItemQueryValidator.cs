using PWMS.Application.Addresses.Models;
using PWMS.Application.Addresses.Queries.Get;
using PWMS.Application.Common.Paging;

namespace PWMS.Application.Configurations.Inventory.Items.Items.Queries.Get;

internal sealed class GetAddressQueryValidator
    : PagingQueryValidator<GetAddressQuery, Result<CollectionViewModel<AddressDto>>>
{
}

