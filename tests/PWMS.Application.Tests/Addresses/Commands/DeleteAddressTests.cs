﻿using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PWMS.Application.Addresses.Commands.Delete;
using PWMS.Application.Addresses.Repositories;
using PWMS.Application.Common.Exceptions;
using PWMS.Application.Tests.Common;
using PWMS.Common.Tests;
using PWMS.Domain.Addresses.Entities;

namespace PWMS.Application.Tests.Addresses.Commands;

[Collection("QueryCollection")]
public class DeleteAddressTests : TestBase
{
    private readonly QueryTestFixture _fixture;

    public DeleteAddressTests(QueryTestFixture fixture) : base(fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Delete_ValidCommand_ShouldCreateSuccess()
    {
        // Arrange
        Guid addressId = _fixture.Context.AppDbContext.Set<Address>()
            .First()
            .Id;
        var command = new DeleteAddressCommand(addressId);

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
