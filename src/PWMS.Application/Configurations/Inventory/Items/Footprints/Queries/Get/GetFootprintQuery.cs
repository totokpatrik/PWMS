using PWMS.Application.Common.Paging;
using PWMS.Application.Configurations.Inventory.Items.Footprints.Models;

namespace PWMS.Application.Configurations.Inventory.Items.Footprints.Queries.Get;

public sealed class GetFootprintQuery : PagingQuery<Result<CollectionViewModel<FootprintDto>>>
{
    public GetFootprintQuery(IPageContext pageContext) : base(pageContext) { }
    public static GetFootprintQuery Create(PageContext pageContext) => new(pageContext);
}
