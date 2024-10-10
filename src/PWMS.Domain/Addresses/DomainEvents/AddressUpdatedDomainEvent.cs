using System;

namespace PWMS.Domain.Addresses.DomainEvents;

public class AddressUpdatedDomainEvent(
    Guid id,
    string addressLine) : AddressBaseEvent(id, addressLine);