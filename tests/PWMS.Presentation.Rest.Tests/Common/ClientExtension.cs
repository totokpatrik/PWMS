using Microsoft.IdentityModel.Tokens;
using PWMS.Application.Addresses.Commands.Create;
using PWMS.Application.Auth.Commands.Login;
using PWMS.Domain.Auth.Entities;
using PWMS.Presentation.Rest.Models.Result;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PWMS.Presentation.Rest.Tests.Common;

public static class ClientExtension
{
    public static RestClient Authenticate(this RestClient client)
    {
        // send request to login endpoint
        var loginCommand = new LoginCommand("Admin", "secret");
        var loginResponse = client.Post<ResultDto<Token>>(
            new RestRequest("api/v1/auth/login").AddJsonBody(loginCommand));

        ArgumentNullException.ThrowIfNull(loginResponse);

        client.AddDefaultHeader("Authorization", $"bearer {loginResponse.Data.TokenString}");

        return client;
    }
}
