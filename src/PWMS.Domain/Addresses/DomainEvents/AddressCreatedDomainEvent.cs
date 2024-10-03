using PWMS.Domain.Abstractions.DomainEvents;
using PWMS.Domain.Abstractions.Entities;

namespace PWMS.Domain.Addresses.DomainEvents;

public sealed record AddressCreatedDomainEvent(string Name) : IDomainEvent;