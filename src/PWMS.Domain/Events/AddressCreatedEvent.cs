using PWMS.Domain.Abstractions;
using PWMS.Domain.Models;

namespace PWMS.Domain.Events;
public record AddressCreatedEvent(Address Address) : IDomainEvent;
