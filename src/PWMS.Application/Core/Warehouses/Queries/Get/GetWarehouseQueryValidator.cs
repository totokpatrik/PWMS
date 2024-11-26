using PWMS.Application.Common.Paging;
using PWMS.Application.Core.Warehouses.Models;

namespace PWMS.Application.Core.Warehouses.Queries.Get;

public sealed class GetWarehouseQueryValidator
: PagingQueryValidator<GetWarehouseQuery, Result<CollectionViewModel<WarehouseDto>>>
{
}
