using Microsoft.AspNetCore.Components;
using PWMS.Application.Addresses.Models;
using PWMS.Web.Blazor.Services.Configuration;

namespace PWMS.Web.Blazor.Pages.Configuration;

public partial class CreateAddress
{
    [Inject] IAddressService AddressService { get; set; } = default!;
    [Inject] NavigationManager NavigationManager { get; set; } = default!;
    CreateAddressDto asd = new() { };
    protected async Task Create(CreateAddressDto createAddressDto)
    {
        await AddressService.CreateAsync(createAddressDto);
        NavigationManager.NavigateTo("/configuration/address");
    }
    protected void Cancel()
    {
        NavigationManager.NavigateTo("/configuration/address");
    }
}