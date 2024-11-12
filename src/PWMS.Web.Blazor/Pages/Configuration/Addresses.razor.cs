using Microsoft.AspNetCore.Components;
using PWMS.Application.Addresses.Models;
using PWMS.Web.Blazor.Services.Configuration;

namespace PWMS.Web.Blazor.Pages.Configuration;

public partial class Addresses
{
    [Inject] IAddressService AddressService { get; set; } = default!;

    IEnumerable<AddressDto> _addresses = new List<AddressDto>();

    protected override async Task OnInitializedAsync()
    {
        var result = await AddressService.GetAddressesAsync(new Application.Common.Paging.PageContext(1, 10));

        if (result.IsSuccess)
        {
            _addresses = result.Data.Data;
        }
    }
}