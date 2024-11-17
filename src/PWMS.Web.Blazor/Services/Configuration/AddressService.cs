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
        if (result.IsSuccess)
        {
            _snackbar.Add("Address created successfully.", Severity.Success);
        }
        else
        {
            _snackbar.Add("There was an error: " + result.Errors, Severity.Error);
        }
        return result;
    }

    public async Task<Result<Guid>> DeleteAsync(DeleteAddressDto deleteAddressDto)
    {
        var result = await _httpService.Delete<Result<Guid>>("api/v1/addresses", deleteAddressDto);
        if (result.IsSuccess)
        {
            _snackbar.Add("Address deleted successfully.", Severity.Success);
        }
        else
        {
            _snackbar.Add("There was an error: " + result.Errors, Severity.Error);
        }
        return result;
    }

    public async Task<Result<List<Guid>>> DeleteRangeAsync(List<DeleteAddressDto> deleteAddressDtos)
    {
        var result = await _httpService.Delete<Result<List<Guid>>>("api/v1/addresses/range", deleteAddressDtos);
        if (result.IsSuccess)
        {
            _snackbar.Add("Addresses deleted successfully.", Severity.Success);
        }
        else
        {
            _snackbar.Add("There was an error: " + result.Errors, Severity.Error);
        }
        return result;
    }

    public async Task<Result<AddressDto>> GetAddressAsync(Guid addressId)
    {
        var result = await _httpService.Get<Result<AddressDto>>($"api/v1/addresses/{addressId}");
        if (!result.IsSuccess)
        {
            _snackbar.Add("There was an error: " + result.Errors, Severity.Error);
        }
        return result;
    }

    public async Task<Result<CollectionViewModel<AddressDto>>> GetAddressesAsync(PageContext pageContext)
    {
        var result = await _httpService.Post<Result<CollectionViewModel<AddressDto>>>("api/v1/addresses/page", pageContext);
        return result;
    }

    public async Task<Result<AddressDto>> UpdateAddressAsync(UpdateAddressDto updateAddressDto)
    {
        var result = await _httpService.Put<Result<AddressDto>>("api/v1/addresses", updateAddressDto);
        if (result.IsSuccess)
        {
            _snackbar.Add("Address updated successfully.", Severity.Success);
        }
        else
        {
            _snackbar.Add("There was an error: " + result.Errors, Severity.Error);
        }
        return result;
    }
}
