using PWMS.Domain.Abstractions.DomainEvents;

namespace PWMS.Domain.Abstractions.Entities;

public abstract class Aggregate : Entity
{
    protected Aggregate() : this(Guid.NewGuid()) { }

    protected Aggregate(Guid id)
    {
        Id = id;
    }

    private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}