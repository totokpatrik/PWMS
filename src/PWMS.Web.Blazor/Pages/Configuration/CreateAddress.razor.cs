using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using PWMS.Application.Addresses.Models;
using PWMS.Web.Blazor.Services;
using PWMS.Web.Blazor.Services.Configuration;

namespace PWMS.Web.Blazor.Pages.Configuration;

public partial class CreateAddress
{
    [Inject] IAddressService AddressService { get; set; } = default!;
    [Inject] NavigationManager NavigationManager { get; set; } = default!;
    CreateAddressDto _createAddressDto = new();

    private MudForm _form = new();
    private bool _loading = false;
    private bool success;

    private FluentValueValidator<string> _addressLineValidator = new FluentValueValidator<string>(x => x
        .NotEmpty()
        .Length(1, 100));

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
            await AddressService.CreateAsync(_createAddressDto);
            NavigationManager.NavigateTo("/configuration/address");
            _loading = false;
        }
    }
    protected void Cancel()
    {
        NavigationManager.NavigateTo("/configuration/address");
    }
}