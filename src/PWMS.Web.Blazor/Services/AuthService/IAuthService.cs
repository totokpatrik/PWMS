using PWMS.Application.Auth.Models;
using PWMS.Domain.Auth.Entities;

namespace PWMS.Web.Blazor.Services.AuthService;

public interface IAuthService
{
    Task<Token> Login(LoginDto request);
}
