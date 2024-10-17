namespace PWMS.Domain.Addresses.Events;

public sealed record AddressCreatedDomainEvent(Guid Id, string addressLine) : INotification;
