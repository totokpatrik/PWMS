using TestCommon.Addresses;

namespace PWMS.Domain.UnitTests.Addresses;

public class AddressTests
{
    [Theory]
    [InlineData("address_name", "pwms@pwms.com", "test address line", "test country", "test state", "test zip code")]
    public void CreateAddress_WhenConstructedSuccessfully(string name, string emailAddress, string addressLine, string country, string state, string zipCode)
    {
        // Arrange
        var address = AddressFactory.CreateAddress(name, emailAddress, addressLine, country, state, zipCode);

        //Assert
        address.DomainEvents.Should().HaveCount(1);
    }

    [Theory]
    [InlineData("", "pwms@pwms.com", "test address line", "test country", "test state", "test zip code")]
    [InlineData("address_name", "", "test address line", "test country", "test state", "test zip code")]
    public void CreateAddress_WhenConstructedNotSuccessfully(string name, string emailAddress, string addressLine, string country, string state, string zipCode)
    {
        // Arrange & Assert
        Assert.Throws<ArgumentException>(() => AddressFactory.CreateAddress(name, emailAddress, addressLine, country, state, zipCode));
    }
}
