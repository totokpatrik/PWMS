using Microsoft.AspNetCore.Components;
using MudBlazor;
using PWMS.Application.Common.Paging;
using PWMS.Application.Configurations.Inventory.Items.ItemFamilyGroups.Models;
using PWMS.Web.Blazor.Models;
using PWMS.Web.Blazor.Services.Configurations.Inventory.Item.ItemFamilyGroup;

namespace PWMS.Web.Blazor.Pages.Configurations.Inventory.Item.ItemFamilyGroup;

public partial class IndexItemFamilyGroup
{
    [Inject] IItemFamilyGroupService ItemFamilyGroupService { get; set; } = default!;
    [Inject] ISnackbar Snackbar { get; set; } = default!;
    [Inject] NavigationManager NavigationManager { get; set; } = default!;

    MudDataGrid<ItemFamilyGroupDto> _dataGrid = new();
    HashSet<ItemFamilyGroupDto> _selected = [];

    int _pageSize = 10;
    int _pageIndex = 1;
    int _totalItems;

    bool _loading = false;
    bool _deleteDisabled = true;

    private async Task<Result<CollectionViewModel<ItemFamilyGroupDto>>> GetPageAsync()
    {
        _loading = true;
        var result = await ItemFamilyGroupService.GetPageAsync(new Application.Common.Paging.PageContext(_pageIndex, _pageSize));

        if (result.IsSuccess)
        {
            _totalItems = result.Data.TotalCount;
            StateHasChanged();
            _loading = false;
        }
        return result;
    }
    private async Task<GridData<ItemFamilyGroupDto>> ServerReload(GridState<ItemFamilyGroupDto> state)
    {
        var result = await GetPageAsync();
        return new GridData<ItemFamilyGroupDto>
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
            List<DeleteItemFamilyGroupDto> deleteDtos = new List<DeleteItemFamilyGroupDto>();
            foreach (var item in _selected)
            {
                var deleteDto = new DeleteItemFamilyGroupDto() { Id = item.Id };
                deleteDtos.Add(deleteDto);
            }
            await ItemFamilyGroupService.DeleteRangeAsync(deleteDtos);
        }
        else
        {
            await ItemFamilyGroupService.DeleteAsync(new DeleteItemFamilyGroupDto { Id = _selected.Select(a => a.Id).First() });
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
        NavigationManager.NavigateTo("/configuration/inventory/item/itemFamilyGroup/create");
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