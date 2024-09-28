namespace PWMS.Domain.Abstractions;

public interface IAggregate<T> : IAggregate, IEntity<T>
{

}
public interface IAggregate
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    IDomainEvent[] ClearDomainEvents();
}
