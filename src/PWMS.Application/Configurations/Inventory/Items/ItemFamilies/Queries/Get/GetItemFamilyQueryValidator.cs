using PWMS.Application.Common.Paging;
using PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Models;

namespace PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Queries.Get;
internal sealed class GetItemFamilyQueryValidator
    : PagingQueryValidator<GetItemFamilyQuery, Result<CollectionViewModel<ItemFamilyDto>>>
{
}
