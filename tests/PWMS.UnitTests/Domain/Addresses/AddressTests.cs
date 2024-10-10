using Bogus;
using FluentAssertions;
using PWMS.Domain.Addresses.Aggregates;
using PWMS.Domain.Addresses.DomainEvents;
using PWMS.Domain.Addresses.Factories;
using Xunit.Categories;

namespace PWMS.UnitTests.Domain.Addresses;

[UnitTest]
public class AddressTests
{
    [Fact]
    public void Should_AddressCreatedDomainEvent_WhenCreate()
    {
        // Arrange
        var addressFaker = new Faker<Address>()
            .CustomInstantiator(faker => AddressFactory.Create(
                faker.Address.FullAddress()));

        // Act
        var act = addressFaker.Generate();

        // Assert
        act.DomainEvents.Should()
            .NotBeNullOrEmpty()
            .And.OnlyHaveUniqueItems()
            .And.ContainItemsAssignableTo<AddressCreatedDomainEvent>();

        act.Id.Should().NotBeEmpty();
    }
    [Fact]
    public void Should_AddressUpdatedDomainEvent_WhenUpdate()
    {
        // Arrange
        var addressEntity = new Faker<Address>()
            .CustomInstantiator(faker => AddressFactory.Create(
                faker.Address.FullAddress()))
            .Generate();

        var updatedAddress = new Faker()
            .Address.FullAddress();

        // Act
        addressEntity.Update(updatedAddress);

        // Assert
        addressEntity.DomainEvents.Should()
            .NotBeNullOrEmpty()
            .And.OnlyHaveUniqueItems()
            .And.ContainItemsAssignableTo<AddressUpdatedDomainEvent>();

    }
    [Fact]
    public void Should_AddressDeletedDomainEvent_WhenDelete()
    {
        // Arrange
        var addressEntity = new Faker<Address>()
            .CustomInstantiator(faker => AddressFactory.Create(
                faker.Address.FullAddress()))
            .Generate();

        // Act
        addressEntity.Delete();

        // Assert
        addressEntity.DomainEvents.Should()
            .NotBeNullOrEmpty()
            .And.OnlyHaveUniqueItems()
            .And.ContainItemsAssignableTo<AddressDeletedDomainEvent>();
    }
    [Fact]
    public void Should_DomainEventsEmpty_WhenCleared()
    {
        // Arrange
        var addressEntity = new Faker<Address>()
            .CustomInstantiator(faker => AddressFactory.Create(
                faker.Address.FullAddress()))
            .Generate();

        // Act
        addressEntity.ClearDomainEvents();

        // Assert
        addressEntity.DomainEvents.Should()
            .BeNullOrEmpty();
    }
}
