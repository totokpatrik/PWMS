using Microsoft.AspNetCore.Components;
using MudBlazor;
using PWMS.Application.Addresses.Models;
using PWMS.Application.Common.Paging;
using PWMS.Web.Blazor.Models;
using PWMS.Web.Blazor.Services.Configuration;

namespace PWMS.Web.Blazor.Pages.Configurations.Inventory.Items.Footprints;

public partial class IndexFootprint
{
    [Inject] IAddressService AddressService { get; set; } = default!;
    [Inject] ISnackbar Snackbar { get; set; } = default!;
    [Inject] NavigationManager NavigationManager { get; set; } = default!;

    MudDataGrid<AddressDto> _dataGrid = new();
    HashSet<AddressDto> _selectedAddresses = [];

    int _pageSize = 10;
    int _pageIndex = 1;
    int _totalItems;

    bool _loading = false;
    bool _deleteDisabled = true;

    private async Task<Result<CollectionViewModel<AddressDto>>> GetAddressesAsync()
    {
        _loading = true;
        var result = await AddressService.GetAddressesAsync(new Application.Common.Paging.PageContext(_pageIndex, _pageSize));

        if (result.IsSuccess)
        {
            _totalItems = result.Data.TotalCount;
            StateHasChanged();
            _loading = false;
        }
        return result;
    }
    private async Task<GridData<AddressDto>> ServerReload(GridState<AddressDto> state)
    {
        var result = await GetAddressesAsync();
        return new GridData<AddressDto>
        {
            TotalItems = result.Data.TotalCount,
            Items = result.Data.Data
        };
    }
    async void Delete()
    {
        _loading = true;
        if (_selectedAddresses.Count > 1)
        {
            List<DeleteAddressDto> deleteAddressDtos = new List<DeleteAddressDto>();
            foreach (var item in _selectedAddresses)
            {
                var deleteAddressDto = new DeleteAddressDto() { Id = item.Id };
                deleteAddressDtos.Add(deleteAddressDto);
            }
            await AddressService.DeleteRangeAsync(deleteAddressDtos);
        }
        else
        {
            await AddressService.DeleteAsync(new DeleteAddressDto { Id = _selectedAddresses.Select(a => a.Id).First() });
        }
        await GetAddressesAsync();

        // clear selection
        _selectedAddresses.Clear();

        // reload data grid
        await _dataGrid.ReloadServerData();
        StateHasChanged();

        _loading = false;
    }
    void Add()
    {
        NavigationManager.NavigateTo("/configuration/address/create");
    }
    private void RefreshGrid()
    {
        _dataGrid.ReloadServerData();
    }

    protected override bool ShouldRender()
    {
        _deleteDisabled = _selectedAddresses.Count < 1;
        return base.ShouldRender();
    }
}