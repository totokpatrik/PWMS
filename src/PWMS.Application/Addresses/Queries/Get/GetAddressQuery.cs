using PWMS.Application.Addresses.Models;
using PWMS.Application.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMS.Application.Addresses.Queries.Get;

public sealed class GetAddressQuery : PagingQuery<Result<CollectionViewModel<AddressDto>>, AddressFilter>
{
    public GetAddressQuery(IPageContext<AddressFilter> pageContext) : base(pageContext)
    {
    }

    public static GetAddressQuery Create(PageContext<AddressFilter> pageContext) => new(pageContext);
}
