using FluentAssertions;
using PWMS.Application.Addresses.Commands.Create;
using PWMS.Application.Tests.Common;
using PWMS.Domain.Addresses.Entities;

namespace PWMS.Application.Tests.Addresses.Commands;

[Collection("QueryCollection")]
public class CreateAddressTests : TestBase
{

    public CreateAddressTests(QueryTestFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task Should_Create_Address()
    {
        var command = new CreateAddressCommand("Test address", AddressType.InboundAddress);
        var result = await Mediator.Send(command);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
    }
}
