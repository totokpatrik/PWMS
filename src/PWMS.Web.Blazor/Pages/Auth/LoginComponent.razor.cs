namespace PWMS.Web.Blazor.Pages.Auth
{
    public partial class LoginComponent
    {

        void Login()
        {
            AuthService.Login(new Application.Auth.Models.LoginDto { Password = "test", UserName = "test" });
        }
    }
}