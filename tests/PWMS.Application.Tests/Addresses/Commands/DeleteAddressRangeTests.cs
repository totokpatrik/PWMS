using FluentAssertions;
using PWMS.Application.Addresses.Commands.Delete;
using PWMS.Application.Addresses.Commands.DeleteRange;
using PWMS.Application.Common.Exceptions;
using PWMS.Application.Tests.Common;
using PWMS.Domain.Addresses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMS.Application.Tests.Addresses.Commands;

[Collection("QueryCollection")]
public class DeleteAddressRangeTests : TestBase
{
    private readonly QueryTestFixture _fixture;

    public DeleteAddressRangeTests(QueryTestFixture fixture) : base(fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Delete_ValidCommand_ShouldCreateSuccess()
    {
        // Arrange
        List<Guid> addressIds = _fixture.Context.AppDbContext.Set<Address>()
            .Take(2)
            .Select(a => a.Id)
            .ToList();
        var command = new DeleteRangeAddressCommand(addressIds);

        // Act
        var result = await Mediator.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
    [Fact]
    public async Task Delete_ValidCommand_NonExistingAddress_ShouldCreateNotFoundException()
    {
        // Arrange
        var command = new DeleteRangeAddressCommand(new List<Guid>() { Guid.NewGuid(), Guid.NewGuid() });

        // Act
        Func<Task> act = () => Mediator.Send(command);

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => act());
    }
}
