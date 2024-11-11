using Blazored.LocalStorage;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using MudBlazor;
using PWMS.Application.Auth.Models;
using PWMS.Web.Blazor.Services;
using PWMS.Web.Blazor.Services.AuthService;

namespace PWMS.Web.Blazor.Pages.Auth
{
    public partial class LoginComponent
    {
        [Inject] ILocalStorageService LocalStorage { get; set; } = default!;
        [Inject] IAuthService AuthService { get; set; } = default!;
        [Inject] NavigationManager NavigationManager { get; set; } = default!;
        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        private MudForm _form = new();
        private bool _success;
        private LoginDto _model = new();
        private string[] _errors = { };
        private bool _loading = false;
        private string returnUrl = string.Empty;

        FluentValueValidator<string> userNameValidator = new FluentValueValidator<string>(x => x
            .NotEmpty()
            .Length(1, 100));

        FluentValueValidator<string> passwordValidator = new FluentValueValidator<string>(x => x
            .NotEmpty()
            .Length(1, 100));

        protected override void OnInitialized()
        {
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("returnUrl", out var url))
            {
                returnUrl = url!;
            }
            base.OnInitialized();
        }

        private async Task Login()
        {
            _loading = true;
            await _form.Validate();

            if (_form.IsValid)
            { 
                var loginResult = await AuthService.Login(_model);

                if (loginResult.IsSuccess)
                {
                    await LocalStorage.SetItemAsync("authToken", loginResult.Data.TokenString);
                    await AuthenticationStateProvider.GetAuthenticationStateAsync();
                    NavigationManager.NavigateTo(returnUrl);
                } else
                {
                    _errors = loginResult.Errors!.Select(e => e.Message!).ToArray();
                }

            }
            _loading = false;
            _success = false;
        }
        private void ClearErrors()
        {
            _errors = [];
            _success = true;
        }
    }
}