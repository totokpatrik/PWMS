using FluentAssertions;
using MediatR;
using PWMS.Application.Auth.Commands.Login;
using PWMS.Application.Auth.Commands.Register;
using PWMS.Domain.Auth.Entities;
using PWMS.Presentation.Rest.Models.Result;
using PWMS.Presentation.Rest.Tests.Common;
using RestSharp;
using System.Net;

namespace PWMS.Presentation.Rest.Tests.Controllers;

[Collection(nameof(RestCollectionDefinition))]
public class AuthControllerTests
{
    private readonly RestWebApplicationFactory<Program> _factory;
    private const string ApiUrlBaseV1 = "api/v1/auth";

    public AuthControllerTests(RestWebApplicationFactory<Program> factory) => _factory = factory;

    private static class Post
    {
        public static string Login() => $"{ApiUrlBaseV1}/login";
        public static string Register() => $"{ApiUrlBaseV1}/register";
    }
    [Fact]
    public async Task LoginUser_Successfull()
    {
        var client = new RestClient(_factory.CreateClient());

        var command = new LoginCommand("TestAdmin", "secret");

        var response = await client.PostAsync<ResultDto<Token>>(
            new RestRequest(Post.Login()).AddJsonBody(command));

        response.Should().NotBeNull();
        response.Should().BeOfType<ResultDto<Token>>();
        response!.Data.TokenString.Should().NotBeNull()
            .And.NotBeNullOrWhiteSpace();
        response!.IsSuccess.Should().BeTrue();
    }
    [Fact]
    public async Task RegisterUser_Successfull()
    {
        var username = "NewUser";
        var password = "Secretpassword123!";
        var client = new RestClient(_factory.CreateClient());
        var command = new RegisterCommand(username, password);

        var response = await client.PostAsync<ResultDto<Token>>(
            new RestRequest(Post.Register()).AddJsonBody(command));

        response.Should().NotBeNull();
        response.Should().BeOfType<ResultDto<Token>>();
        response!.Data.TokenString.Should().NotBeNull()
            .And.NotBeNullOrWhiteSpace();
        response!.IsSuccess.Should().BeTrue();
    }
    [Fact]
    public async Task LoginUser_IncorrectPassword()
    {
        var client = new RestClient(_factory.CreateClient());

        var command = new LoginCommand("Admin", "incorrect_password");

        // Act
        var response = await client.ExecutePostAsync<ResultDto<Unit>>(
            new RestRequest(Post.Login()).AddJsonBody(command));

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        response.IsSuccessful.Should().BeFalse();
        response.IsSuccessStatusCode.Should().BeFalse();

        response.Data!.IsSuccess.Should().BeFalse();
        response.Data!.Errors.Should().ContainSingle();
    }
    [Fact]
    public async Task LoginUser_NonExistingUser()
    {
        var client = new RestClient(_factory.CreateClient());
        var command = new LoginCommand("Nonexistinguser", "incorrect_password");

        // Act
        var response = await client.ExecutePostAsync<ResultDto<Unit>>(
            new RestRequest(Post.Login()).AddJsonBody(command));

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        response.IsSuccessful.Should().BeFalse();
        response.IsSuccessStatusCode.Should().BeFalse();

        response.Data!.IsSuccess.Should().BeFalse();
        response.Data!.Errors.Should().ContainSingle();
    }

    [Fact]
    public async Task RegisterUser_AlreadyExistingUser()
    {
        var username = "TestAdmin";
        var password = "Secretpassword123!";
        var client = new RestClient(_factory.CreateClient());
        var command = new RegisterCommand(username, password);

        var response = await client.ExecutePostAsync<ResultDto<Unit>>(
            new RestRequest(Post.Register()).AddJsonBody(command));

        response.Should().NotBeNull();
        response.IsSuccessful.Should().BeFalse();
        response.Data!.IsSuccess.Should().BeFalse();
        response.Data!.Errors.Should().ContainSingle();
        response.Data!.Errors.First().Code.Should().Be("DuplicateUserName");
        response.Data!.Errors.First().Message.Should().Be("Username 'TestAdmin' is already taken.");
    }
}
