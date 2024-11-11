using PWMS.Application.Auth.Models;
using PWMS.Domain.Auth.Entities;
using PWMS.Web.Blazor.Models;
using System.Net.Http.Json;

namespace PWMS.Web.Blazor.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly HttpClient _http;

    public AuthService(HttpClient http)
    {
        _http = http;
    }
    public async Task<Result<Token>> Login(LoginDto request)
    {
        var result = await _http.PostAsJsonAsync("api/v1/auth/login", request);

        var response = await result.Content.ReadFromJsonAsync<Result<Token>>();

        if (response == null)
        {
            throw new Exception();
        }

        return response;
    }
}
