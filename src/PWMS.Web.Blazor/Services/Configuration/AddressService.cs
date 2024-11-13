using PWMS.Application.Addresses.Models;
using PWMS.Application.Common.Paging;
using PWMS.Web.Blazor.Models;
using PWMS.Web.Blazor.Services.HttpService;

namespace PWMS.Web.Blazor.Services.Configuration;

public class AddressService : IAddressService
{
    private readonly IHttpService _httpService;

    public AddressService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<Result<CollectionViewModel<AddressDto>>> GetAddressesAsync(PageContext pageContext)
    {
        var result = await _httpService.Post<Result<CollectionViewModel<AddressDto>>>("api/v1/addresses/page", pageContext);

        if (result == null)
        {
            throw new Exception();
        }

        return result;
    }
}
