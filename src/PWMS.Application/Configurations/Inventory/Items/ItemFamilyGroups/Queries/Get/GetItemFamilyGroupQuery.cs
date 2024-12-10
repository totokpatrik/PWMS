using PWMS.Application.Common.Paging;
using PWMS.Application.Configurations.Inventory.Items.ItemFamilyGroups.Models;

namespace PWMS.Application.Configurations.Inventory.Items.ItemFamilyGroups.Queries.Get;

public sealed class GetItemFamilyGroupQuery : PagingQuery<Result<CollectionViewModel<ItemFamilyGroupDto>>>
{
    public GetItemFamilyGroupQuery(IPageContext pageContext) : base(pageContext)
    {
    }

    public static GetItemFamilyGroupQuery Create(PageContext pageContext) => new(pageContext);
}
