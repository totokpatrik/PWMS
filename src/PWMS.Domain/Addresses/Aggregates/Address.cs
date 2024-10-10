using PWMS.Core.SharedKernel;
using PWMS.Domain.Addresses.DomainEvents;

namespace PWMS.Domain.Addresses.Aggregates;

public class Address : BaseEntity
{
    private bool _isDeleted;
    /// <summary>
    /// Initializes a new instance of the Address class.
    /// </summary>
    /// <param name="addressLine">The address line of the address.</param>
    public Address(string addressLine)
    {
        AddressLine = addressLine;

        AddDomainEvent(new AddressCreatedDomainEvent(Id, AddressLine));
    }

    /// <summary>
    /// Default constructor for Entity Framework or other ORM frameworks.
    /// </summary>
    public Address()
    {
    }

    // Properties
    /// <summary>
    /// Gets the address line of the address.
    /// </summary>
    public string AddressLine { get; private set; }

    /// <summary>
    /// Updates the address.
    /// </summary>
    /// <param name="addressLine">The new address line.</param>
    public void Update(string addressLine)
    {
        AddressLine = addressLine;

        AddDomainEvent(new AddressUpdatedDomainEvent(Id, AddressLine));
    }

    /// <summary>
    /// Deletes the address.
    /// </summary>
    public void Delete()
    {
        if (_isDeleted) return;

        _isDeleted = true;
        AddDomainEvent(new AddressDeletedDomainEvent(Id, AddressLine));
    }
}
