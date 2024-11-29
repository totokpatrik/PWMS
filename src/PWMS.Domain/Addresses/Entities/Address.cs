using PWMS.Domain.Addresses.Events;
using PWMS.Domain.Common;

namespace PWMS.Domain.Addresses.Entities;

public class Address : BaseAuditableWarehouseEntity<Guid, string>, IAggregateRoot
{
    public string AddressLine { get; set; }
    public AddressType AddressType { get; set; } = AddressType.InboundAddress;

    public Address(Guid id, string addressLine, AddressType addressType) : base(id)
    {
        AddressLine = addressLine;
        AddressType = addressType;

        AddCreateDomainEvent();
    }
    public Address(string addressLine, AddressType addressType) : base(Guid.NewGuid())
    {
        AddressLine = addressLine;
        AddressType = addressType;

        AddCreateDomainEvent();
    }

    public void Update(string addressLine, AddressType addressType)
    {
        AddressLine = addressLine;
        AddressType = addressType;
    }
    private void AddCreateDomainEvent()
    {
        var createEvent = new AddressCreatedDomainEvent(Id, AddressLine);
        AddDomainEvent(createEvent);
    }
}
