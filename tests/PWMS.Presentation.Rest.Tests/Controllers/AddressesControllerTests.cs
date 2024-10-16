using FluentAssertions;
using PWMS.Application.Addresses.Commands.Create;
using PWMS.Presentation.Rest.Models.Result;
using PWMS.Presentation.Rest.Tests.Common;
using RestSharp;

namespace PWMS.Presentation.Rest.Tests.Controllers;

[Collection(nameof(RestCollectionDefinition))]
public class AddressesControllerTests
{
    private const string ApiUrlBaseV1 = "api/v1/addresses";
    private const string ApiUrlBaseV2 = "api/v2/addresses";

    private readonly RestWebApplicationFactory<Program> _factory;

    public AddressesControllerTests(RestWebApplicationFactory<Program> factory) => _factory = factory;


    private static class Post
    {
        public static string CreateAddressV1() => $"{ApiUrlBaseV1}";
        public static string CreateAddressV2() => $"{ApiUrlBaseV2}";
    }

    [Fact]
    public async Task CreateTestAsync()
    {
        var client = new RestClient(_factory.CreateClient());

        var command = new CreateAddressCommand("Test Address");

        var response = await client.PostAsync<ResultDto<Guid>>(
            new RestRequest(Post.CreateAddressV1()).AddJsonBody(command));

        response.Should().NotBeNull();
        response.Should().BeOfType<ResultDto<Guid>>();
        response!.IsSuccess.Should().BeTrue();
    }
}
