using PWMS.Application.Common.Paging;
using PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Models;

namespace PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Queries.Get;

internal sealed class GetItemFamilyGroupQueryValidator
    : PagingQueryValidator<GetItemFamilyGroupQuery, Result<CollectionViewModel<ItemFamilyGroupDto>>>
{
}
