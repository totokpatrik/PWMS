using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using PWMS.Application.Common.Paging;
using PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Models;
using PWMS.Application.Configurations.Inventory.Items.ItemFamilyGroups.Models;
using PWMS.Web.Blazor.Models;
using PWMS.Web.Blazor.Services.Configurations.Inventory.Item.ItemFamily;
using PWMS.Web.Blazor.Services.Configurations.Inventory.Item.ItemFamilyGroup;

namespace PWMS.Web.Blazor.Pages.Configurations.Inventory.Item.ItemFamily;

public partial class CreateItemFamily
{
    [Inject] IItemFamilyService ItemFamilyService { get; set; } = default!;
    [Inject] IItemFamilyGroupService ItemFamilyGroupService { get; set; } = default!;
    [Inject] NavigationManager NavigationManager { get; set; } = default!;
    CreateItemFamilyDto _createItemFamilyDto = new();
    ItemFamilyGroupDto _itemFamilyGroupDto = default!;

    private MudForm _form = new();
    private bool _loading = false;
    private bool _success;

    private string[] _state =
    {
        "Alabama", "Alaska", "American Samoa", "Arizona"
    };

    private async Task FormKeyDownAsync(KeyboardEventArgs e)
    {

        if (e.Code == "Enter" || e.Code == "NumpadEnter")
        {
            await Create();
        }
    }
    private async Task<IEnumerable<ItemFamilyGroupDto>> Search(string value, CancellationToken token)
    {
        var pageContext = new PageContext(1, 10);

        var result = new Result<CollectionViewModel<ItemFamilyGroupDto>>();

        if (!string.IsNullOrEmpty(value))
        {
            pageContext.Filter = new Castle.DynamicLinqQueryBuilder.QueryBuilderFilterRule
            {
                Condition = "AND",
                Field = "Name",
                Type = "string",
                Operator = "contains",
                Value = [value]
            };
        }

        result = await ItemFamilyGroupService.GetPageAsync(pageContext);

        return result.Data.Data;
    }
    protected async Task Create()
    {
        if (_form.IsValid)
        {
            _loading = true;

            _createItemFamilyDto.ItemfamilyGroupId = _itemFamilyGroupDto.Id;

            await ItemFamilyService.CreateAsync(_createItemFamilyDto);
            NavigationManager.NavigateTo("/configuration/inventory/item/itemFamily");
            _loading = false;
        }
    }
    protected void Cancel()
    {
        NavigationManager.NavigateTo("/configuration/inventory/item/itemFamily");
    }
}