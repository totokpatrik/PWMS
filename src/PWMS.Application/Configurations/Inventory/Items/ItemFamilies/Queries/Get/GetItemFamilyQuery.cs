using PWMS.Application.Common.Paging;
using PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Models;

namespace PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Queries.Get;

public sealed class GetItemFamilyQuery : PagingQuery<Result<CollectionViewModel<ItemFamilyDto>>>
{
    public GetItemFamilyQuery(IPageContext pageContext) : base(pageContext) { }
    public static GetItemFamilyQuery Create(PageContext pageContext) => new(pageContext);
}
