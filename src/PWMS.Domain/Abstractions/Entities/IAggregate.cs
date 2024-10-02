using PWMS.Domain.Abstractions.DomainEvents;

namespace PWMS.Domain.Abstractions.Entities;

public interface IAggregate<T> : IAggregate, IEntity
{

}
public interface IAggregate
{
    IReadOnlyCollection<DomainEvent> DomainEvents { get; }
    IDomainEvent[] ClearDomainEvents();
}
