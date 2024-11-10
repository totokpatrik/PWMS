using PWMS.Application.Auth.Models;
using PWMS.Domain.Auth.Entities;
using System.Net.Http.Json;

namespace PWMS.Web.Blazor.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly HttpClient _http;

    public AuthService(HttpClient http)
    {
        _http = http;
    }
    public async Task<Token> Login(LoginDto request)
    {
        var result = await _http.PostAsJsonAsync("api/auth/login", request);

        var response = await result.Content.ReadFromJsonAsync<Token>();

        if (response == null)
        {
            throw new Exception();
        }

        return response;
    }
}
