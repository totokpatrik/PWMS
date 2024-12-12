using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using PWMS.Application.Configurations.Inventory.Items.ItemFamilyGroups.Models;
using PWMS.Web.Blazor.Services.Configurations.Inventory.Item.ItemFamilyGroup;

namespace PWMS.Web.Blazor.Pages.Configurations.Inventory.Item.ItemFamilyGroup;

public partial class CreateItemFamilyGroup
{
    [Inject] IItemFamilyGroupService ItemFamilyGroupService { get; set; } = default!;
    [Inject] NavigationManager NavigationManager { get; set; } = default!;
    CreateItemFamilyGroupDto _createItemFamilyGroupDto = new();

    private MudForm _form = new();
    private bool _loading = false;
    private bool _success;

    private async Task FormKeyDownAsync(KeyboardEventArgs e)
    {

        if (e.Code == "Enter" || e.Code == "NumpadEnter")
        {
            await Create();
        }
    }
    protected async Task Create()
    {
        if (_form.IsValid)
        {
            _loading = true;
            await ItemFamilyGroupService.CreateAsync(_createItemFamilyGroupDto);
            NavigationManager.NavigateTo("/configuration/inventory/item/itemFamilyGroup");
            _loading = false;
        }
    }
    protected void Cancel()
    {
        NavigationManager.NavigateTo("/configuration/inventory/item/itemFamilyGroup");
    }
}