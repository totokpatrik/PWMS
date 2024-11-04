using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PWMS.Application.Addresses.Commands.Delete;
using PWMS.Application.Addresses.Repositories;
using PWMS.Application.Common.Exceptions;
using PWMS.Application.Tests.Common;
using PWMS.Common.Tests;

namespace PWMS.Application.Tests.Addresses.Commands;

[Collection("QueryCollection")]
public class DeleteAddressTests : TestBase
{
    public DeleteAddressTests(QueryTestFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task Delete_ValidCommand_ShouldCreateSuccess()
    {
        // Arrange
        var command = new DeleteAddressCommand(Guid.Parse("AB7D57B6-0EB1-4E7C-9147-A84B254034C4"));

        // Act
        var result = await Mediator.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
    [Fact]
    public async Task Delete_ValidCommand_NonExistingAddress_ShouldCreateNotFoundException()
    {
        // Arrange
        var command = new DeleteAddressCommand(Guid.NewGuid());

        // Act
        Func<Task> act = () => Mediator.Send(command);

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => act());
    }
}
