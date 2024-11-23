using MudBlazor;
using PWMS.Application.Common.Paging;
using PWMS.Application.Core.Warehouses.Models;
using PWMS.Domain.Auth.Entities;
using PWMS.Web.Blazor.Models;
using PWMS.Web.Blazor.Services.HttpService;

namespace PWMS.Web.Blazor.Services.Core;

public class WarehouseService : IWarehouseService
{
    private readonly IHttpService _httpService;
    private readonly ISnackbar _snackbar;

    public WarehouseService(IHttpService httpService, ISnackbar Snackbar)
    {
        _httpService = httpService;
        _snackbar = Snackbar;
    }
    public async Task<Result<Guid>> CreateAsync(CreateWarehouseDto createWarehouseDto)
    {
        var result = await _httpService.Post<Result<Guid>>("api/v1/warehouses", createWarehouseDto);
        if (result.IsSuccess)
        {
            _snackbar.Add("Warehouse created successfully.", Severity.Success);
        }
        else
        {
            _snackbar.Add("There was an error: " + result.Errors, Severity.Error);
        }
        return result;
    }

    public Task<Result<Guid>> DeleteAsync(DeleteWarehouseDto deleteWarehouseDto)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<Guid>>> DeleteRangeAsync(List<DeleteWarehouseDto> deleteWarehouseDtos)
    {
        throw new NotImplementedException();
    }

    public Task<Result<WarehouseDto>> GetWarehouseAsync(Guid warehouseId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<CollectionViewModel<WarehouseDto>>> GetWarehousesAsync(PageContext pageContext)
    {
        var result = await _httpService.Post<Result<CollectionViewModel<WarehouseDto>>>("api/v1/warehouses/page", pageContext);
        return result;
    }

    public async Task<Result<Token>> SelectWarehouseAsync(SelectWarehouseDto selectWarehouseDto)
    {
        var result = await _httpService.Post<Result<Token>>("api/v1/warehouses/select", selectWarehouseDto);
        if (result.IsSuccess)
        {
            _snackbar.Add("Site selected successfully.", Severity.Success);
        }
        else
        {
            var errorList = "";
            foreach (var error in result.Errors)
            {
                errorList += error.Message + "\n";
            }
            _snackbar.Add("There was an error: " + errorList, Severity.Error);
        }
        return result;
    }

    public Task<Result<WarehouseDto>> UpdateWarehouseAsync(UpdateWarehouseDto updateWarehouseDto)
    {
        throw new NotImplementedException();
    }
}
