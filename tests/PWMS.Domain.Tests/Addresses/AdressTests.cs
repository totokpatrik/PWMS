using Bogus;
using FluentAssertions;
using PWMS.Domain.Addresses.Entities;
using PWMS.Domain.Addresses.Events;

namespace PWMS.Domain.Tests.Addresses;

public class AdressTests
{
    [Fact]
    public void Should_AddressCreatedDomainEvent_WhenCreate()
    {
        // Arrange
        var addressFaker = new Faker<Address>()
            .CustomInstantiator(faker => new Address(faker.Address.FullAddress()));

        // Act
        var act = addressFaker.Generate();

        // Assert
        act.DomainEvents.Should()
            .NotBeNullOrEmpty()
            .And.OnlyHaveUniqueItems()
            .And.ContainItemsAssignableTo<AddressCreatedDomainEvent>();

        act.Id.Should().NotBeEmpty();
    }
}
