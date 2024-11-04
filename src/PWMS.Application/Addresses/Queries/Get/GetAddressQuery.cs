using PWMS.Application.Addresses.Models;
using PWMS.Application.Common.Paging;

namespace PWMS.Application.Addresses.Queries.Get;

public sealed class GetAddressQuery : PagingQuery<Result<CollectionViewModel<AddressDto>>>
{
    public GetAddressQuery(IPageContext pageContext) : base(pageContext)
    {
    }

    public static GetAddressQuery Create(PageContext pageContext) => new(pageContext);
}
