using PWMS.Application.Auth.Models;
using PWMS.Domain.Auth.Entities;
using PWMS.Web.Blazor.Models;

namespace PWMS.Web.Blazor.Services.AuthService;

public interface IAuthService
{
    Task<Result<Token>> Login(LoginDto request);
}
