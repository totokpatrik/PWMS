using Microsoft.AspNetCore.Components;
using MudBlazor;
using PWMS.Application.Addresses.Models;
using PWMS.Application.Common.Paging;
using PWMS.Web.Blazor.Models;
using PWMS.Web.Blazor.Services.Configuration;

namespace PWMS.Web.Blazor.Pages.Configuration;

public partial class Addresses
{
    [Inject] IAddressService AddressService { get; set; } = default!;

    MudDataGrid<AddressDto> dataGrid = new();
    List<AddressDto> selectedAddresses = new List<AddressDto>();
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

    void SelectedItemsChanged(HashSet<AddressDto> addresses)
    {
        selectedAddresses = addresses.ToList();
        deleteDisabled = selectedAddresses.Count < 1;
    }
}