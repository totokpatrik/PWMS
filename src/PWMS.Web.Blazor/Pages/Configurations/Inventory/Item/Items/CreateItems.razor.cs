using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using PWMS.Application.Configurations.Inventory.Items.Items.Models;
using PWMS.Web.Blazor.Services.Configurations.Inventory.Item;

namespace PWMS.Web.Blazor.Pages.Configurations.Inventory.Item.Items;

public partial class CreateItems
{
    [Inject] IItemService ItemService { get; set; } = default!;
    [Inject] NavigationManager NavigationManager { get; set; } = default!;
    CreateItemDto _createItemDto = new();

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
            await ItemService.CreateAsync(_createItemDto);
            NavigationManager.NavigateTo("/configuration/inventory/item/item");
            _loading = false;
        }
    }
    protected void Cancel()
    {
        NavigationManager.NavigateTo("/configuration/inventory/item/item");
    }
}