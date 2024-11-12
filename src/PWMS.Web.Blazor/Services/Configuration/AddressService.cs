using PWMS.Application.Addresses.Models;
using PWMS.Application.Common.Paging;
using PWMS.Web.Blazor.Models;
using System.Net.Http.Json;

namespace PWMS.Web.Blazor.Services.Configuration;

public class AddressService : IAddressService
{
    private readonly HttpClient _http;

    public AddressService(HttpClient http)
    {
        _http = http;
    }

    public async Task<Result<CollectionViewModel<AddressDto>>> GetAddressesAsync(PageContext pageContext)
    {
        var result = await _http.PostAsJsonAsync("api/v1/addresses/page", pageContext);

        var asd = result.Content;

        var lol = await result.Content.ReadAsStringAsync();

        var response = await result.Content.ReadFromJsonAsync<Result<CollectionViewModel<AddressDto>>>();

        if (response == null)
        {
            throw new Exception();
        }

        return response;
    }
}
