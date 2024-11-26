using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using PWMS.Application.Core.Warehouses.Models;
using PWMS.Web.Blazor.Services;
using PWMS.Web.Blazor.Services.Core;

namespace PWMS.Web.Blazor.Pages.Core.Warehouse;

public partial class CreateWarehouse
{
    [Inject] IWarehouseService WarehouseService { get; set; } = default!;
    [Inject] NavigationManager NavigationManager { get; set; } = default!;
    CreateWarehouseDto _createWarehouseDto = new();

    private MudForm _form = new();
    private bool _loading = false;
    private bool success;

    private FluentValueValidator<string> _warehouseNameValidator = new FluentValueValidator<string>(x => x
        .NotEmpty()
        .Length(1, 20));

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
            await WarehouseService.CreateAsync(_createWarehouseDto);
            NavigationManager.NavigateTo("/warehouse");
            _loading = false;
        }
    }
    protected void Cancel()
    {
        NavigationManager.NavigateTo("/warehouse");
    }
}