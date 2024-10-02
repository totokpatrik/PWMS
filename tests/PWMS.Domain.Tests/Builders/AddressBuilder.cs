using PWMS.Domain.Addresses.Entities;

namespace PWMS.Domain.Tests.Builders;

public class AddressBuilder
{
    private string _name = "Test Address";
    private string _emailAddress = "pwms@pwms.com";
    private string _addressLine = "Test Address Line";
    private string _country = "Test Country";
    private string _state = "Test State";
    private string _zipCode = "Test ZipCode";

    public Address Build()
    {
        return Address.Create(_name, _emailAddress, _addressLine, _country, _state, _zipCode);
    }

    public AddressBuilder WithName(string name)
    {
        _name = name;
        return this;
    }
    public AddressBuilder WithEmailAddress(string emailAddress)
    {
        _emailAddress = emailAddress;
        return this;
    }
    public AddressBuilder WithAddressLine(string addressLine)
    {
        _addressLine = addressLine;
        return this;
    }
    public AddressBuilder WithCountry(string country)
    {
        _country = country;
        return this;
    }
    public AddressBuilder WithState(string state)
    {
        _state = state;
        return this;
    }
    public AddressBuilder WithZipCode(string zipCode)
    {
        _zipCode = zipCode;
        return this;
    }
}
