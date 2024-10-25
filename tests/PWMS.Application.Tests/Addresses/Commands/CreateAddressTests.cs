using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PWMS.Application.Addresses.Commands.Create;
using PWMS.Application.Addresses.Commands.Delete;
using PWMS.Application.Addresses.Repositories;
using PWMS.Application.Tests.Common;
using PWMS.Common.Tests;

namespace PWMS.Application.Tests.Addresses.Commands;

[Collection("QueryCollection")]
public class CreateAddressTests : TestBase
{

    public CreateAddressTests(QueryTestFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task Create_ValidCommand_WithDelay_ShouldCreateSuccess()
    {
        var services = new ServiceCollection();

        var serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(IAddressRepository));
        services.Remove(serviceDescriptor);

        var serviceProvider = services
            .AddScoped<IAddressRepository, MockAddressRepository>()
            .BuildServiceProvider();

        var command = new CreateAddressCommand("TestAddress");

        // Act
        var result = await Mediator.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();

    }

    [Fact]
    public async Task Should_Create_Address()
    {
        var command = new CreateAddressCommand("asd");
        var result = await Mediator.Send(command);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
    }
}
