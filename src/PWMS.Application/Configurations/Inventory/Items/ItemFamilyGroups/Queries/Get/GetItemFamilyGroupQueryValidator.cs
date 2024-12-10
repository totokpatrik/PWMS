using PWMS.Application.Common.Paging;
using PWMS.Application.Configurations.Inventory.Items.ItemFamilyGroups.Models;

namespace PWMS.Application.Configurations.Inventory.Items.ItemFamilyGroups.Queries.Get;

internal sealed class GetItemFamilyGroupQueryValidator
    : PagingQueryValidator<GetItemFamilyGroupQuery, Result<CollectionViewModel<ItemFamilyGroupDto>>>
{
}
