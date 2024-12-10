using Microsoft.AspNetCore.Components;
using MudBlazor;
using PWMS.Application.Addresses.Models;
using PWMS.Application.Common.Paging;
using PWMS.Web.Blazor.Models;
using PWMS.Web.Blazor.Services.Configuration;

namespace PWMS.Web.Blazor.Pages.Configurations;

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

    bool _firstPageButtonDisabled;
    bool _navigateBeforeButtonDisabled;
    bool _navigateNextButtonDisabled;
    bool _lastPageButtonDisabled;

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
        var result = await AddressService.GetAddressesAsync(new PageContext(_pageIndex, _pageSize));

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
        StateHasChanged();

        _loading = false;
    }
    void Add()
    {
        NavigationManager.NavigateTo("/configuration/address/create");
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
        _pageIndex = (int)Math.Ceiling(_totalItems / (decimal)_pageSize);
        await dataGrid.ReloadServerData();
    }
    private async Task NavigateToNextPage()
    {
        _pageIndex++;
        await dataGrid.ReloadServerData();

    }
}