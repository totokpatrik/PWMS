using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using PWMS.Application.Common.Paging;
using PWMS.Application.Core.Warehouses.Models;
using PWMS.Web.Blazor.Models;
using PWMS.Web.Blazor.Services.Core;

namespace PWMS.Web.Blazor.Pages.Core.Warehouse;

public partial class IndexWarehouse
{
    [Inject] IWarehouseService WarehouseService { get; set; } = default!;
    [Inject] ISnackbar Snackbar { get; set; } = default!;
    [Inject] NavigationManager NavigationManager { get; set; } = default!;
    [Inject] ILocalStorageService LocalStorage { get; set; } = default!;
    [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

    MudDataGrid<WarehouseDto> dataGrid = new();
    HashSet<WarehouseDto> selectedWarehouses = new HashSet<WarehouseDto>();
    int _pageSize = 10;
    int _pageIndex = 1;
    int _totalItems = 0;
    int[] _pageSizeOptions = [10, 25, 50, 100];
    bool _loading = false;

    bool _firstPageButtonDisabled;
    bool _navigateBeforeButtonDisabled;
    bool _navigateNextButtonDisabled;
    bool _lastPageButtonDisabled;

    bool deleteDisabled = true;
    bool selectDisabled = true;

    string currentUserId = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        currentUserId = (await AuthenticationStateProvider.GetAuthenticationStateAsync())
            .User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value ?? string.Empty;
        await GetWarehousesAsync();
    }
    private Task PageSizeChangedAsync(int pageSize)
    {
        _pageSize = pageSize;
        return dataGrid.ReloadServerData();
    }

    private async Task<Result<CollectionViewModel<WarehouseDto>>> GetWarehousesAsync()
    {
        _loading = true;
        var result = await WarehouseService.GetWarehousesAsync(new PageContext(_pageIndex, _pageSize));

        if (result.IsSuccess)
        {
            _totalItems = result.Data.TotalCount;
            _loading = false;
        }
        return result;
    }
    private async Task<GridData<WarehouseDto>> ServerReload(GridState<WarehouseDto> state)
    {
        var result = await GetWarehousesAsync();
        return new GridData<WarehouseDto>
        {
            TotalItems = result.Data.TotalCount,
            Items = result.Data.Data
        };
    }
    protected override bool ShouldRender()
    {
        deleteDisabled = selectedWarehouses.Count < 1;
        selectDisabled = selectedWarehouses.Count != 1;
        if (_pageIndex == 1)
        {
            _firstPageButtonDisabled = true;
            _navigateBeforeButtonDisabled = true;
        }
        else
        {
            _firstPageButtonDisabled = false;
            _navigateBeforeButtonDisabled = false;
        }

        if (_pageIndex * _pageSize >= _totalItems)
        {
            _navigateNextButtonDisabled = true;
            _lastPageButtonDisabled = true;
        }
        else
        {
            _navigateNextButtonDisabled = false;
            _lastPageButtonDisabled = false;
        }


        return base.ShouldRender();
    }
    async void Delete()
    {
        _loading = true;
        if (selectedWarehouses.Count > 1)
        {
            List<DeleteWarehouseDto> deleteWarehousesDto = new List<DeleteWarehouseDto>();
            foreach (var item in selectedWarehouses)
            {
                var deleteWarehouseDto = new DeleteWarehouseDto() { Id = item.Id };
                deleteWarehousesDto.Add(deleteWarehouseDto);
            }
            await WarehouseService.DeleteRangeAsync(deleteWarehousesDto);
        }
        else
        {
            await WarehouseService.DeleteAsync(new DeleteWarehouseDto { Id = selectedWarehouses.Select(a => a.Id).First() });
        }
        await GetWarehousesAsync();

        // clear selection
        selectedWarehouses.Clear();

        // reload data grid
        await dataGrid.ReloadServerData();
        StateHasChanged();

        _loading = false;
    }
    void Add()
    {
        NavigationManager.NavigateTo("/warehouse/create");
    }
    async Task SelectAsync()
    {
        var selectWarehouseResult = await WarehouseService.SelectWarehouseAsync(new SelectWarehouseDto { WarehouseId = selectedWarehouses.First().Id });
        if (selectWarehouseResult.IsSuccess)
        {
            await LocalStorage.SetItemAsync("authToken", selectWarehouseResult.Data.TokenString);
            await AuthenticationStateProvider.GetAuthenticationStateAsync();
        }
        // clear selection
        selectedWarehouses.Clear();

        // reload data grid
        await dataGrid.ReloadServerData();
        StateHasChanged();
        NavigationManager.Refresh(true);
    }
    private async Task NavigateToFirstPageAsync()
    {
        _pageIndex = 1;
        await dataGrid.ReloadServerData();
    }
    private async Task NavigateToPreviousPage()
    {
        _pageIndex--;
        await dataGrid.ReloadServerData();
    }
    private async Task NavigateToLastPage()
    {
        _pageIndex = (int)Math.Ceiling((decimal)(_totalItems / (decimal)_pageSize));
        await dataGrid.ReloadServerData();
    }
    private async Task NavigateToNextPage()
    {
        _pageIndex++;
        await dataGrid.ReloadServerData();

    }
}