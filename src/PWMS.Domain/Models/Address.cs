using PWMS.Domain.Abstractions;
using PWMS.Domain.Events;
using PWMS.Domain.Models.ValueObjects;

namespace PWMS.Domain.Models;

public class Address : Aggregate<AddressId>
{
    public string Name { get; private set; } = default!;
    public string EmailAddress { get; private set; } = default!;
    public string AddressLine { get; private set; } = default!;
    public string Country { get; private set; } = default!;
    public string State { get; private set; } = default!;
    public string ZipCode { get; private set; } = default!;

    public static Address Create(AddressId id, string name, string emailAddress, string addressLine, string country, string state, string zipCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(emailAddress);
        ArgumentException.ThrowIfNullOrWhiteSpace(addressLine);
        ArgumentException.ThrowIfNullOrWhiteSpace(country);
        ArgumentException.ThrowIfNullOrWhiteSpace(state);
        ArgumentException.ThrowIfNullOrWhiteSpace(zipCode);

        var address = new Address
        {
            Id = id,
            Name = name,
            EmailAddress = emailAddress,
            AddressLine = addressLine,
            Country = country,
            State = state,
            ZipCode = zipCode
        };

        address.AddDomainEvent(new AddressCreatedEvent(address));

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

        AddDomainEvent(new AddressUpdatedEvent(this));
    }

    public void Remove(AddressId id)
    {
        AddDomainEvent(new AddressDeletedEvent(this));
    }
}
