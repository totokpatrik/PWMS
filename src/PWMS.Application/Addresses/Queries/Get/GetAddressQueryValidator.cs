using PWMS.Application.Addresses.Models;
using PWMS.Application.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMS.Application.Addresses.Queries.Get;

internal sealed class GetAddressQueryValidator
    : PagingQueryValidator<GetAddressQuery, Result<CollectionViewModel<AddressDto>>, AddressFilter>
{
}
