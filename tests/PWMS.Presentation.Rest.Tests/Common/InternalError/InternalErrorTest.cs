using FluentAssertions;
using MediatR;
using PWMS.Application.Addresses.Commands.Create;
using PWMS.Domain.Addresses.Entities;
using PWMS.Presentation.Rest.Models.Result;
using RestSharp;

namespace PWMS.Presentation.Rest.Tests.Common.InternalError;
[Collection(nameof(InternalErrorCollectionDefinition))]
public class InternalErrorTest
{
    private const string ApiUrlBaseV1 = "api/v1/addresses";
    private readonly InternalErrorWebApplicationFactory<Program> _factory;
    public InternalErrorTest(InternalErrorWebApplicationFactory<Program> factory) => _factory = factory;

    private static class Post
    {
        public static string CreateAddressV1() => $"{ApiUrlBaseV1}";
    }


    [Fact]
    public async Task CreateTestAsync()
    {
        var client = new RestClient(_factory.CreateClient()).Authenticate();

        var command = new CreateAddressCommand("Test Address", AddressType.InboundAddress);

        var response = await client.ExecutePostAsync<ResultDto<Unit>>(
            new RestRequest(Post.CreateAddressV1()).AddJsonBody(command));

        response.Should().NotBeNull();
    }
}
