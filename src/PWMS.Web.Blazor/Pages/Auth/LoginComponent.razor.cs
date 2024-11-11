using FluentValidation;
using MudBlazor;
using PWMS.Application.Auth.Models;
using PWMS.Web.Blazor.Services;

namespace PWMS.Web.Blazor.Pages.Auth
{
    public partial class LoginComponent
    {
        MudForm form;
        bool success;
        LoginDto model = new LoginDto();

        FluentValueValidator<string> userNameValidator = new FluentValueValidator<string>(x => x
            .NotEmpty()
            .Length(1, 100));

        FluentValueValidator<string> passwordValidator = new FluentValueValidator<string>(x => x
            .NotEmpty()
            .Length(1, 100));

        private async Task Login()
        {
            AuthService.Login(new Application.Auth.Models.LoginDto { Password = "test", UserName = "test" });
        }
    }
}