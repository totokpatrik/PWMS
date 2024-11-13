using Microsoft.AspNetCore.Components;
using PWMS.Application.Addresses.Models;
using PWMS.Application.Common.Paging;
using PWMS.Web.Blazor.Models;
using PWMS.Web.Blazor.Services.Configuration;

namespace PWMS.Web.Blazor.Pages.Configuration;

public partial class Addresses
{
    [Inject] IAddressService AddressService { get; set; } = default!;

    IEnumerable<AddressDto> _addresses = new List<AddressDto>();

    Result<CollectionViewModel<AddressDto>> _addressResult = new Result<CollectionViewModel<AddressDto>>();
    int _pageSize = 10;
    int _pageIndex = 1;
    bool _loading = false;

    protected override async Task OnInitializedAsync()
    {
        _loading = true;
        var result = await AddressService.GetAddressesAsync(new Application.Common.Paging.PageContext(_pageIndex, _pageSize));

        if (result.IsSuccess)
        {
            _addressResult = result;
            _addresses = result.Data.Data;
            _loading = false;
        }
    }
}