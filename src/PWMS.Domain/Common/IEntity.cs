﻿namespace PWMS.Domain.Common;

public interface IEntity
{
    bool IsNew { get; }

    IReadOnlyCollection<INotification> DomainEvents { get; }

    void AddDomainEvent(INotification domainEvent);

    void ClearDomainEvents();
}
