namespace Portal.Shared.Core.Interfaces;

public interface IDomainEventContext
{
    public IEnumerable<IDomainEvent> GetDomainEvents();
}