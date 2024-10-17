using Bogus;
using FluentAssertions;
using PWMS.Application.Addresses.Commands.Update;
using PWMS.Application.Tests.Common;
using ValidationException = PWMS.Application.Common.Exceptions.ValidationException;

namespace PWMS.Application.Tests.Addresses.Commands;

[Collection("QueryCollection")]
public class UpdateAddressTests : TestBase
{
    public UpdateAddressTests(QueryTestFixture fixture) : base(fixture)
    {
    }
    [Fact]
    public async Task Update_ValidCommand_ShouldCreateSuccess()
    {
        // Arrange
        var updatedAddressLine = new Faker().Address.FullAddress();
        var command = new UpdateAddressCommand(Guid.Parse("AB7D57B6-0EB1-4E7C-9147-A84B254034C4"), updatedAddressLine);

        // Act
        var result = await Mediator.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.AddressLine.Should().Be(updatedAddressLine);
    }
    [Fact]
    public async Task Update_InvalidCommand_EmptyAddressLine_ShouldCreateSuccess()
    {
        // Arrange
        var command = new UpdateAddressCommand(Guid.NewGuid(), "");

        // Act
        Func<Task> act = () => Mediator.Send(command);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(() => act());
    }
}
