using FluentAssertions;
using PWMS.Domain.Abstractions.Exceptions;
using PWMS.Domain.Addresses.DomainEvents;
using PWMS.Domain.Tests.Builders;

namespace PWMS.Domain.Tests.Addresses.Entities;

public class AddressTests
{
    [Fact]
    public void GivenAddress_WhenCreate_ThenCreate()
    {
        // Arrange & Act
        var address = new AddressBuilder().Build();

        // Assert
        address.Name.Should().NotBeNull();
        address.EmailAddress.Should().NotBeNull();
        address.AddressLine.Should().NotBeNull();
        address.Country.Should().NotBeNull();
        address.State.Should().NotBeNull();
        address.ZipCode.Should().NotBeNull();

        address.DomainEvents.Where(e => e is AddressCreatedDomainEvent).Should().HaveCount(1);
    }
    [Fact]
    public void GivenAddress_WhenCreateEmptyName_ThenError()
    {
        // Arrange & Act
        Action action = () => new AddressBuilder().WithName("").Build();

        // Assert
        action.Should().Throw<DomainException>();
    }
    [Fact]
    public void GivenAddress_WhenCreateEmptyEmail_ThenError()
    {
        // Arrange & Act
        Action action = () => new AddressBuilder().WithEmailAddress("").Build();

        // Assert
        action.Should().Throw<DomainException>();
    }
    [Fact]
    public void GivenAddress_WhenCreateEmptyAddressLine_ThenError()
    {
        // Arrange & Act
        Action action = () => new AddressBuilder().WithAddressLine("").Build();

        // Assert
        action.Should().Throw<DomainException>();
    }
    [Fact]
    public void GivenAddress_WhenCreateEmptyCountry_ThenError()
    {
        // Arrange & Act
        Action action = () => new AddressBuilder().WithCountry("").Build();

        // Assert
        action.Should().Throw<DomainException>();
    }
    [Fact]
    public void GivenAddress_WhenCreateEmptyState_ThenError()
    {
        // Arrange & Act
        Action action = () => new AddressBuilder().WithState("").Build();

        // Assert
        action.Should().Throw<DomainException>();
    }
    [Fact]
    public void GivenAddress_WhenCreateEmptyZipCode_ThenError()
    {
        // Arrange & Act
        Action action = () => new AddressBuilder().WithZipCode("").Build();

        // Assert
        action.Should().Throw<DomainException>();
    }
    [Fact]
    public void GivenAddress_WhenUpdate_ThenUpdate()
    {
        // Arrange
        var address = new AddressBuilder().Build();
        string updatedName = "Updated Address";
        string updatedEmailAddress = "updated.pwms@pwms.com";
        string updatedAddressLine = "Updated Address Line";
        string updatedCountry = "Updated Country";
        string updatedState = "Updated State";
        string updatedZipCode = "Updated ZipCode";

        // Act
        address.Update(name: updatedName, emailAddress: updatedEmailAddress, addressLine: updatedAddressLine, country: updatedCountry,
            state: updatedState, zipCode: updatedZipCode);

        // Assert
        address.Name.Should().Be(updatedName);
        address.EmailAddress.Should().Be(updatedEmailAddress);
        address.AddressLine.Should().Be(updatedAddressLine);
        address.Country.Should().Be(updatedCountry);
        address.State.Should().Be(updatedState);
        address.ZipCode.Should().Be(updatedZipCode);
    }
}
