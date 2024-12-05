using PWMS.Application.Common.Paging;
using PWMS.Application.Configuration.Inventory.Items.ItemFamilies.Models;

namespace PWMS.Application.Configuration.Inventory.Items.ItemFamilies.Queries.Get;
internal sealed class GetItemFamilyQueryValidator
    : PagingQueryValidator<GetItemFamilyQuery, Result<CollectionViewModel<ItemFamilyDto>>>
{
}
