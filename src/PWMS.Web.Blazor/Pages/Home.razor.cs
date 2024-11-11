using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace PWMS.Web.Blazor.Pages
{
    public partial class Home
    {
        [Inject] NavigationManager NavigationManager { get; set; } = default!;
        [Inject] AuthenticationStateProvider AuthStateProvider { get; set; } = default!;

        [CascadingParameter]
        private Task<AuthenticationState>? AuthState { get; set; }

        protected override async void OnInitialized()
        {
            if (!(await AuthStateProvider.GetAuthenticationStateAsync()).User.Identity!.IsAuthenticated)
            {
                NavigationManager.NavigateTo("/login");
            }

            base.OnInitialized();
        }

    }
}