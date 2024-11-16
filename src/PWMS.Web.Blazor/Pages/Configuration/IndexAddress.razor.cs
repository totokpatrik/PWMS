using Microsoft.AspNetCore.Components;
using MudBlazor;
using PWMS.Application.Addresses.Models;
using PWMS.Application.Common.Paging;
using PWMS.Web.Blazor.Models;
using PWMS.Web.Blazor.Services.Configuration;

namespace PWMS.Web.Blazor.Pages.Configuration;

public partial class IndexAddress
{
    [Inject] IAddressService AddressService { get; set; } = default!;
    [Inject] ISnackbar Snackbar { get; set; } = default!;
    [Inject] NavigationManager NavigationManager { get; set; } = default!;

    MudDataGrid<AddressDto> dataGrid = new();
    HashSet<AddressDto> selectedAddresses = new HashSet<AddressDto>();
    int _pageSize = 10;
    int _pageIndex = 1;
    int _totalItems = 0;
    int[] _pageSizeOptions = [10, 25, 50, 100];
    bool _loading = false;

    bool deleteDisabled = true;

    protected override async Task OnInitializedAsync()
    {
        await GetAddressesAsync();
    }
    private Task PageSizeChangedAsync(int pageSize)
    {
        _pageSize = pageSize;
        return dataGrid.ReloadServerData();
    }

    private async Task<Result<CollectionViewModel<AddressDto>>> GetAddressesAsync()
    {
        _loading = true;
        var result = await AddressService.GetAddressesAsync(new Application.Common.Paging.PageContext(_pageIndex, _pageSize));

        if (result.IsSuccess)
        {
            _totalItems = result.Data.TotalCount;
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
    protected override bool ShouldRender()
    {
        deleteDisabled = selectedAddresses.Count < 1;
        return base.ShouldRender();
    }
    async void Delete()
    {
        _loading = true;
        if (selectedAddresses.Count > 1)
        {
            List<DeleteAddressDto> deleteAddressDtos = new List<DeleteAddressDto>();
            foreach (var item in selectedAddresses)
            {
                var deleteAddressDto = new DeleteAddressDto() { Id = item.Id };
                deleteAddressDtos.Add(deleteAddressDto);
            }
            await AddressService.DeleteRangeAsync(deleteAddressDtos);
        }
        else
        {
            await AddressService.DeleteAsync(new DeleteAddressDto { Id = selectedAddresses.Select(a => a.Id).First() });
        }
        await GetAddressesAsync();

        // clear selection
        selectedAddresses.Clear();

        // reload data grid
        await dataGrid.ReloadServerData();

        _loading = false;
    }
    void Add()
    {
        NavigationManager.NavigateTo("/configuration/address/create");
    }
}