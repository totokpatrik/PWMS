using FluentAssertions;
using PWMS.Application.Addresses.Commands.Create;
using PWMS.Application.Addresses.Commands.Delete;
using PWMS.Application.Addresses.Commands.Update;
using PWMS.Application.Addresses.Models;
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
    }
    private static class Put
    {
        public static string UpdateAddressV1() => $"{ApiUrlBaseV1}";
    }
    public static class Delete
    {
        public static string DeleteAddressV1() => $"{ApiUrlBaseV1}";
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
    [Fact]
    public async Task UpdateTestAsync()
    {
        var client = new RestClient(_factory.CreateClient());
        var updatedAddressLine = "Updated test address";

        var command = new UpdateAddressCommand(Guid.Parse("4B178375-845F-4D84-9E5B-31A14F097AA1"), updatedAddressLine);

        var response = await client.PutAsync<ResultDto<AddressDto>>(
            new RestRequest(Put.UpdateAddressV1()).AddJsonBody(command));

        response.Should().NotBeNull();
        response.Should().BeOfType<ResultDto<AddressDto>>();
        response!.IsSuccess.Should().BeTrue();
        response.Data.AddressLine.Should().Be(updatedAddressLine);
    }
    [Fact]
    public async Task DeleteTestAsync()
    {
        var client = new RestClient(_factory.CreateClient());
        var addressId = Guid.Parse("4B178375-845F-4D84-9E5B-31A14F097AA1");


        var command = new DeleteAddressCommand(addressId);

        var response = await client.DeleteAsync<ResultDto<Guid>>(
            new RestRequest(Delete.DeleteAddressV1()).AddJsonBody(command));

        response.Should().NotBeNull();
        response.Should().BeOfType<ResultDto<Guid>>();
        response!.IsSuccess.Should().BeTrue();
        response.Data.Should().Be(addressId);
    }
}
