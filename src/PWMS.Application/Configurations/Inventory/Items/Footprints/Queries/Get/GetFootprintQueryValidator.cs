using PWMS.Application.Common.Paging;
using PWMS.Application.Configurations.Inventory.Items.Footprints.Models;

namespace PWMS.Application.Configurations.Inventory.Items.Footprints.Queries.Get;
internal sealed class GetFootprintQueryValidator
: PagingQueryValidator<GetFootprintQuery, Result<CollectionViewModel<FootprintDto>>>
{
}
