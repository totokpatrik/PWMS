﻿namespace PWMS.Domain.Common;

public abstract class BaseEntity<TKey> : IPkEntity<TKey>
{
    private readonly List<INotification> _domainEvents = new();

    protected BaseEntity(TKey id) => Id = id;

    public TKey Id { get; }

    [NotMapped]
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(INotification domainEvent) => _domainEvents.Add(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();
}
