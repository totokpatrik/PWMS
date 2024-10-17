﻿using PWMS.Domain.Common;

namespace PWMS.Persistence.PortgreSQL.Extensions;

public static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync(
        this IMediator mediator, IEnumerable<EntityEntry<IEntity>> entityEntries)
    {
        var enumerable = entityEntries.ToList();

        var domainEvents = enumerable
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        enumerable
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent).ConfigureAwait(false);
        }
    }
}
