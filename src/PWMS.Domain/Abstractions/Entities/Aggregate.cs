using PWMS.Domain.Abstractions.DomainEvents;

namespace PWMS.Domain.Abstractions.Entities;

public abstract class Aggregate : Entity, IAggregate
{
    protected Aggregate() : this(Guid.NewGuid()) { }

    protected Aggregate(Guid id)
    {
        Id = id;
    }

    private readonly List<DomainEvent> _domainEvents = new List<DomainEvent>();
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(DomainEvent eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    IDomainEvent[] IAggregate.ClearDomainEvents()
    {
        throw new NotImplementedException();
    }
}