using PWMS.Application.Common.Paging;
using PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Models;

namespace PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Queries.Get;

internal sealed class GetFootprintDetailQueryValidator
: PagingQueryValidator<GetFootprintDetailQuery, Result<CollectionViewModel<FootprintDetailDto>>>
{
}
