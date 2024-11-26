using PWMS.Application.Auth.Commands.Login;
using PWMS.Domain.Auth.Entities;
using PWMS.Presentation.Rest.Models.Result;
using PWMS.Presentation.Rest.Tests.SeedData;
using RestSharp;

namespace PWMS.Presentation.Rest.Tests.Common;

public static class ClientExtension
{
    public static RestClient Authenticate(this RestClient client)
    {
        // send request to login endpoint
        var loginCommand = new LoginCommand(SeedDataContext.AdminUser.UserName!, "secret");
        var loginResponse = client.Post<ResultDto<Token>>(
            new RestRequest("api/v1/auth/login").AddJsonBody(loginCommand));

        ArgumentNullException.ThrowIfNull(loginResponse);

        client.AddDefaultHeader("Authorization", $"bearer {loginResponse.Data.TokenString}");

        return client;
    }
}
