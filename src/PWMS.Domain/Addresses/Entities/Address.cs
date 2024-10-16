using PWMS.Domain.Addresses.Events;
using PWMS.Domain.Common;

namespace PWMS.Domain.Addresses.Entities;

public class Address : BaseAuditableEntity<Guid, string>, IAggregateRoot
{

    public string AddressLine { get; set; }

    public Address(string addressLine) : base(Guid.NewGuid())
    {
        AddressLine = addressLine;

        AddCreateDomainEvent();
    }

    public void Update(string addressLine)
    {
        AddressLine = addressLine;
    }
    private void AddCreateDomainEvent()
    {
        var createEvent = new AddressCreatedDomainEvent(Id, AddressLine);
        AddDomainEvent(createEvent);
    }
}
