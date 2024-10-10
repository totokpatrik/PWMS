using System;

namespace PWMS.Domain.Addresses.DomainEvents;

public class AddressDeletedDomainEvent(
    Guid id,
    string addressLine) : AddressBaseEvent(id, addressLine);
