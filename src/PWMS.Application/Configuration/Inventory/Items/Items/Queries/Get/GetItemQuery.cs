using PWMS.Application.Common.Paging;
using PWMS.Application.Configuration.Inventory.Items.Items.Models;

namespace PWMS.Application.Configuration.Inventory.Items.Items.Queries.Get;

public sealed class GetItemQuery : PagingQuery<Result<CollectionViewModel<ItemDto>>>
{
    public GetItemQuery(IPageContext pageContext) : base(pageContext) { }
    public static GetItemQuery Create(PageContext pageContext) => new(pageContext);
}
