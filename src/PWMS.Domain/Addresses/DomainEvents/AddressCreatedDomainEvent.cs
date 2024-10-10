using System;

namespace PWMS.Domain.Addresses.DomainEvents;

public class AddressCreatedDomainEvent(
    Guid id,
    string addressLine) : AddressBaseEvent(id, addressLine);
