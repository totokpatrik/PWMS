using PWMS.Application.Common.Paging;
using PWMS.Application.Core.Warehouses.Models;
using PWMS.Domain.Auth.Entities;
using PWMS.Web.Blazor.Models;

namespace PWMS.Web.Blazor.Services.Core;

public interface IWarehouseService
{
    Task<Result<WarehouseDto>> GetWarehouseAsync(Guid warehouseId);
    Task<Result<WarehouseDto>> UpdateWarehouseAsync(UpdateWarehouseDto updateWarehouseDto);
    Task<Result<CollectionViewModel<WarehouseDto>>> GetWarehousesAsync(PageContext pageContext);
    Task<Result<Guid>> CreateAsync(CreateWarehouseDto createWarehouseDto);
    Task<Result<Guid>> DeleteAsync(DeleteWarehouseDto deleteWarehouseDto);
    Task<Result<List<Guid>>> DeleteRangeAsync(List<DeleteWarehouseDto> deleteWarehouseDtos);
    Task<Result<Token>> SelectWarehouseAsync(SelectWarehouseDto selectWarehouseDto);
}
