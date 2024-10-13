using Bogus;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using PWMS.Application.Addresses.Commands.CreateAddress;
using PWMS.Application.Addresses.Repositories;
using PWMS.Core.SharedKernel;
using PWMS.Infrastructure.Addresses.Repositories;
using PWMS.Infrastructure.Data;
using PWMS.UnitTests.Fixtures;

namespace PWMS.UnitTests.Application.Addresses;

public class CreateAddressCommandHandlerTests(EfSqliteFixture fixture) : IClassFixture<EfSqliteFixture>
{
    private readonly CreateAddressCommandValidator _validator = new();

    [Fact]
    public async Task Add_ValidCommand_ShouldReturnSuccessResult()
    {
        // Arrange
        var command = new Faker<CreateAddressCommand>()
            .RuleFor(command => command.AddressLine, faker => faker.Address.FullAddress());

        var unitOfWork = new UnitOfWork(
            fixture.Context,
            Substitute.For<IEventStoreRepository>(),
            Substitute.For<IMediator>(),
            Substitute.For<ILogger<UnitOfWork>>());

        var hanlder = new CreateAddressCommandHandler(
            _validator,
            new AddressRepository(fixture.Context),
            unitOfWork);

        // Act
        var act = await hanlder.Handle(command, CancellationToken.None);

        // Assert
        act.Should().NotBeNull();
        act.IsSuccess.Should().BeTrue();
        act.SuccessMessage.Should().Be("Successfully registered!");
        act.Value.Should().NotBeNull();
        act.Value.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task Add_TooLongAddressLine_ShouldReturnFailResult()
    {
        // Arrange
        var command = new Faker<CreateAddressCommand>()
            .RuleFor(command => command.AddressLine, faker => faker.Random.String(101));

        var unitOfWork = new UnitOfWork(
            fixture.Context,
            Substitute.For<IEventStoreRepository>(),
            Substitute.For<IMediator>(),
            Substitute.For<ILogger<UnitOfWork>>());

        var hanlder = new CreateAddressCommandHandler(
            _validator,
            new AddressRepository(fixture.Context),
            unitOfWork);

        // Act
        var act = await hanlder.Handle(command, CancellationToken.None);

        // Assert
        act.Should().NotBeNull();
        act.IsSuccess.Should().BeFalse();
        act.Value.Should().BeNull();
        act.ValidationErrors.Should().NotBeNullOrEmpty();
    }
    [Fact]
    public async Task Add_InvalidCommand_ShouldReturnFailResult()
    {
        // Arrange
        var hanlder = new CreateAddressCommandHandler(
            _validator,
            Substitute.For<IAddressRepository>(),
            Substitute.For<IUnitOfWork>());

        // Act
        var act = await hanlder.Handle(new CreateAddressCommand(), CancellationToken.None);

        // Assert
        act.Should().NotBeNull();
        act.IsSuccess.Should().BeFalse();
        act.ValidationErrors.Should().NotBeNullOrEmpty().And.OnlyHaveUniqueItems();
    }
}
