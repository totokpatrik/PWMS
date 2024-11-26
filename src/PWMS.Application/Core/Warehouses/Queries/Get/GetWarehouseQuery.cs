using PWMS.Application.Common.Paging;
using PWMS.Application.Core.Warehouses.Models;

namespace PWMS.Application.Core.Warehouses.Queries.Get;

public class GetWarehouseQuery : PagingQuery<Result<CollectionViewModel<WarehouseDto>>>
{
    public GetWarehouseQuery(IPageContext pageContext) : base(pageContext)
    {
    }

    public static GetWarehouseQuery Create(PageContext pageContext) => new(pageContext);
}
