using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using PWMS.Application.Configurations.Inventory.Items.ItemFamilyGroups.Models;
using PWMS.Web.Blazor.Services.Configurations.Inventory.Item.ItemFamilyGroup;

namespace PWMS.Web.Blazor.Pages.Configurations.Inventory.Item.ItemFamilyGroup;

public partial class EditItemFamilyGroup
{
    [Parameter]
    public Guid Id { get; set; }
    [Inject]
    IItemFamilyGroupService ItemFamilyGroupService { get; set; } = default!;
    [Inject]
    NavigationManager NavigationManager { get; set; } = default!;

    UpdateItemFamilyGroupDto _updateItemFamilyGroupDto = new();
    private MudForm _form = new();
    private bool _loading = false;
    private bool _success;

    protected override async Task OnInitializedAsync()
    {
        await GetAddress();
    }

    private async Task GetAddress()
    {
        var result = await ItemFamilyGroupService.GetAsync(Id);

        _updateItemFamilyGroupDto.Id = result.Data.Id;
        _updateItemFamilyGroupDto.Name = result.Data.Name;
        _updateItemFamilyGroupDto.Description = result.Data.Description;
    }
    private async Task FormKeyDownAsync(KeyboardEventArgs e)
    {

        if (e.Code == "Enter" || e.Code == "NumpadEnter")
        {
            await UpdateAsync();
        }
    }

    async Task UpdateAsync()
    {
        if (_form.IsValid)
        {
            _loading = true;
            var result = await ItemFamilyGroupService.UpdateAsync(_updateItemFamilyGroupDto);
            if (result.IsSuccess)
            {
                NavigationManager.NavigateTo("/configuration/inventory/item/itemFamilyGroup");
            }
            _loading = false;
        }
    }

    void Cancel()
    {
        NavigationManager.NavigateTo("/configuration/inventory/item/itemFamilyGroup");
    }
}