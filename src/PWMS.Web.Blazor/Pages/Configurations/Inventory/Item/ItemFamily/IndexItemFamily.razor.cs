using Microsoft.AspNetCore.Components;
using MudBlazor;
using PWMS.Application.Common.Paging;
using PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Models;
using PWMS.Web.Blazor.Models;
using PWMS.Web.Blazor.Services.Configurations.Inventory.Item.ItemFamily;

namespace PWMS.Web.Blazor.Pages.Configurations.Inventory.Item.ItemFamily;

public partial class IndexItemFamily
{
    [Inject] IItemFamilyService ItemFamilyService { get; set; } = default!;
    [Inject] ISnackbar Snackbar { get; set; } = default!;
    [Inject] NavigationManager NavigationManager { get; set; } = default!;

    MudDataGrid<ItemFamilyDto> _dataGrid = new();
    HashSet<ItemFamilyDto> _selected = [];

    int _pageSize = 10;
    int _pageIndex = 1;
    int _totalItems;

    bool _loading = false;
    bool _deleteDisabled = true;

    private async Task<Result<CollectionViewModel<ItemFamilyDto>>> GetPageAsync()
    {
        _loading = true;
        var result = await ItemFamilyService.GetPageAsync(new Application.Common.Paging.PageContext(_pageIndex, _pageSize));

        if (result.IsSuccess)
        {
            _totalItems = result.Data.TotalCount;
            StateHasChanged();
            _loading = false;
        }
        return result;
    }
    private async Task<GridData<ItemFamilyDto>> ServerReload(GridState<ItemFamilyDto> state)
    {
        var result = await GetPageAsync();
        return new GridData<ItemFamilyDto>
        {
            TotalItems = result.Data.TotalCount,
            Items = result.Data.Data
        };
    }
    async void Delete()
    {
        _loading = true;
        if (_selected.Count > 1)
        {
            List<DeleteItemFamilyDto> deleteDtos = new List<DeleteItemFamilyDto>();
            foreach (var item in _selected)
            {
                var deleteDto = new DeleteItemFamilyDto() { Id = item.Id };
                deleteDtos.Add(deleteDto);
            }
            await ItemFamilyService.DeleteRangeAsync(deleteDtos);
        }
        else
        {
            await ItemFamilyService.DeleteAsync(new DeleteItemFamilyDto { Id = _selected.Select(a => a.Id).First() });
        }
        await GetPageAsync();

        // clear selection
        _selected.Clear();

        // reload data grid
        await _dataGrid.ReloadServerData();
        StateHasChanged();

        _loading = false;
    }
    void Add()
    {
        NavigationManager.NavigateTo("/configuration/inventory/item/itemFamily/create");
    }
    private void RefreshGrid()
    {
        _dataGrid.ReloadServerData();
    }

    protected override bool ShouldRender()
    {
        _deleteDisabled = _selected.Count < 1;
        return base.ShouldRender();
    }
}