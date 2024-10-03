﻿using MediatR;

namespace PWMS.Domain.Abstractions.DomainEvents;

public interface IDomainEvent : INotification
{
    Guid EventId => Guid.NewGuid();
    public DateTime OccuredOn => DateTime.Now;
    public string EventType => GetType().AssemblyQualifiedName!;
}