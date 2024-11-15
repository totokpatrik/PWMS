using MudBlazor;
using PWMS.Application.Addresses.Models;
using PWMS.Application.Common.Paging;
using PWMS.Web.Blazor.Models;
using PWMS.Web.Blazor.Services.HttpService;

namespace PWMS.Web.Blazor.Services.Configuration;

public class AddressService : IAddressService
{
    private readonly IHttpService _httpService;
    private readonly ISnackbar _snackbar;

    public AddressService(IHttpService httpService, ISnackbar Snackbar)
    {
        _httpService = httpService;
        _snackbar = Snackbar;
    }

    public async Task<Result<Guid>> CreateAsync(CreateAddressDto createAddressDto)
    {
        var result = await _httpService.Post<Result<Guid>>("api/v1/addresses", createAddressDto);
        return result;
    }

    public async Task<Result<Guid>> DeleteAsync(DeleteAddressDto deleteAddressDto)
    {
        var result = await _httpService.Delete<Result<Guid>>("api/v1/addresses", deleteAddressDto);
        return result;
    }

    public async Task<Result<List<Guid>>> DeleteRangeAsync(List<Guid> ids)
    {
        var result = await _httpService.Delete<Result<List<Guid>>>("api/v1/addresses/range", ids);
        return result;
    }

    public async Task<Result<CollectionViewModel<AddressDto>>> GetAddressesAsync(PageContext pageContext)
    {
        var result = await _httpService.Post<Result<CollectionViewModel<AddressDto>>>("api/v1/addresses/page", pageContext);
        return result;
    }
}
