namespace SaoViet.Portal.Domain.Interfaces;

public interface IAggregateRoot<out T> : IEntity<T> where T : notnull
{
    public HashSet<IDomainEvent> DomainEvents { get; }
}