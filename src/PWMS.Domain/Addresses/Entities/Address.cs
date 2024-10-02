using PWMS.Domain.Abstractions.Entities;
using PWMS.Domain.Abstractions.Guards;
using PWMS.Domain.Addresses.DomainEvents;

namespace PWMS.Domain.Addresses.Entities;

public class Address : Aggregate
{
    public string Name { get; private set; } = default!;
    public string EmailAddress { get; private set; } = default!;
    public string AddressLine { get; private set; } = default!;
    public string Country { get; private set; } = default!;
    public string State { get; private set; } = default!;
    public string ZipCode { get; private set; } = default!;

    public static Address Create(string name, string emailAddress, string addressLine, string country, string state, string zipCode)
    {
        Guard.Against.NullOrEmpty(name);
        Guard.Against.NullOrEmpty(emailAddress);
        Guard.Against.NullOrEmpty(addressLine);
        Guard.Against.NullOrEmpty(country);
        Guard.Against.NullOrEmpty(state);
        Guard.Against.NullOrEmpty(zipCode);

        var address = new Address
        {
            Name = name,
            EmailAddress = emailAddress,
            AddressLine = addressLine,
            Country = country,
            State = state,
            ZipCode = zipCode
        };

        address.AddDomainEvent(new AddressCreatedDomainEvent(name));

        return address;
    }

    public void Update(string name, string emailAddress, string addressLine, string country, string state, string zipCode)
    {
        Name = name;
        EmailAddress = emailAddress;
        AddressLine = addressLine;
        Country = country;
        State = state;
        ZipCode = zipCode;
    }
}
