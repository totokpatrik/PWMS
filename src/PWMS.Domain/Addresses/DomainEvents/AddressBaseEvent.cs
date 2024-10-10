using PWMS.Core.SharedKernel;
using System;

namespace PWMS.Domain.Addresses.DomainEvents;

public abstract class AddressBaseEvent : BaseEvent
{
    protected AddressBaseEvent(
        Guid id,
        string addressLine)
    {
        Id = id;
        AddressLine = addressLine;
    }
    public Guid Id { get; private init; }
    public string AddressLine { get; private init; }
}
