using Microsoft.AspNetCore.Components;
using MudBlazor;
using PWMS.Application.Common.Paging;
using PWMS.Application.Configurations.Inventory.Items.Items.Models;
using PWMS.Web.Blazor.Models;
using PWMS.Web.Blazor.Services.Configurations.Inventory.Item;

namespace PWMS.Web.Blazor.Pages.Configurations.Inventory.Item.Items;

public partial class IndexItems
{
    [Inject] IItemService ItemService { get; set; } = default!;
    [Inject] ISnackbar Snackbar { get; set; } = default!;
    [Inject] NavigationManager NavigationManager { get; set; } = default!;

    MudDataGrid<ItemDto> _dataGrid = new();
    HashSet<ItemDto> _selectedItems = [];

    int _pageSize = 10;
    int _pageIndex = 1;
    int _totalItems;

    bool _loading = false;
    bool _deleteDisabled = true;

    private async Task<Result<CollectionViewModel<ItemDto>>> GetItemsAsync()
    {
        _loading = true;
        var result = await ItemService.GetItemsAsync(new Application.Common.Paging.PageContext(_pageIndex, _pageSize));

        if (result.IsSuccess)
        {
            _totalItems = result.Data.TotalCount;
            StateHasChanged();
            _loading = false;
        }
        return result;
    }
    private async Task<GridData<ItemDto>> ServerReload(GridState<ItemDto> state)
    {
        var result = await GetItemsAsync();
        return new GridData<ItemDto>
        {
            TotalItems = result.Data.TotalCount,
            Items = result.Data.Data
        };
    }
    async void Delete()
    {
        _loading = true;
        if (_selectedItems.Count > 1)
        {
            List<DeleteItemDto> deleteItemDtos = new List<DeleteItemDto>();
            foreach (var item in _selectedItems)
            {
                var deleteAddressDto = new DeleteItemDto() { Id = item.Id };
                deleteItemDtos.Add(deleteAddressDto);
            }
            await ItemService.DeleteRangeAsync(deleteItemDtos);
        }
        else
        {
            await ItemService.DeleteAsync(new DeleteItemDto { Id = _selectedItems.Select(a => a.Id).First() });
        }
        await GetItemsAsync();

        // clear selection
        _selectedItems.Clear();

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
        _deleteDisabled = _selectedItems.Count < 1;
        return base.ShouldRender();
    }
}