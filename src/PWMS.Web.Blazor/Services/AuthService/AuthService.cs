using PWMS.Application.Auth.Models;
using PWMS.Domain.Auth.Entities;
using PWMS.Web.Blazor.Models;
using PWMS.Web.Blazor.Services.HttpService;

namespace PWMS.Web.Blazor.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly IHttpService _httpService;

    public AuthService(IHttpService httpService)
    {
        _httpService = httpService;
    }
    public async Task<Result<Token>> Login(LoginDto request)
    {
        var result = await _httpService.Post<Result<Token>>("api/v1/auth/login", request);

        if (result == null)
        {
            throw new Exception();
        }

        return result;
    }
}
