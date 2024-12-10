using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using PWMS.Application.Addresses.Models;
using PWMS.Web.Blazor.Services;
using PWMS.Web.Blazor.Services.Configuration;

namespace PWMS.Web.Blazor.Pages.Configurations;

public partial class EditAddress
{
    [Parameter]
    public Guid? AddressId { get; set; }
    [Inject]
    IAddressService AddressService { get; set; } = default!;
    [Inject]
    NavigationManager NavigationManager { get; set; } = default!;

    UpdateAddressDto _updateAddressDto = new();
    private MudForm _form = new();
    private bool _loading = false;
    private bool success;

    private FluentValueValidator<string> _addressLineValidator = new FluentValueValidator<string>(x => x
        .NotEmpty()
        .Length(1, 100));

    protected override async Task OnInitializedAsync()
    {
        await GetAddress();
    }

    private async Task GetAddress()
    {
        var address = await AddressService.GetAddressAsync(AddressId!.Value);

        _updateAddressDto.Id = address.Data.Id;
        _updateAddressDto.AddressLine = address.Data.AddressLine;
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
            var result = await AddressService.UpdateAddressAsync(_updateAddressDto);
            if (result.IsSuccess)
            {
                NavigationManager.NavigateTo("/configuration/address");
            }
            _loading = false;
        }
    }

    void Cancel()
    {
        NavigationManager.NavigateTo("/configuration/address");
    }
}