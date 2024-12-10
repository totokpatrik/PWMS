using PWMS.Application.Common.Paging;
using PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Models;

namespace PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Queries.Get;

public sealed class GetFootprintDetailQuery : PagingQuery<Result<CollectionViewModel<FootprintDetailDto>>>
{
    public GetFootprintDetailQuery(IPageContext pageContext) : base(pageContext) { }
    public static GetFootprintDetailQuery Create(PageContext pageContext) => new(pageContext);
}
