namespace Portal.Shared.Core.Interfaces;

public interface IAggregateRoot
{
    public HashSet<IDomainEvent> DomainEvents { get; }
}