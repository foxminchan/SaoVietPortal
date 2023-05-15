using MediatR;
using SaoViet.Portal.Domain.DomainEvents;
using SaoViet.Portal.Domain.Interfaces;

namespace SaoViet.Portal.Domain.AggregateRoot;

public static class Extension
{
    public static async Task RelayAndPublishEvents<T>(
        this IAggregateRoot<T> aggregateRoot,
        IPublisher publisher,
        CancellationToken cancellationToken = default)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(aggregateRoot, nameof(aggregateRoot));

        var domainEvents = new IDomainEvent[aggregateRoot.DomainEvents.Count];
        aggregateRoot.DomainEvents.CopyTo(domainEvents);
        aggregateRoot.DomainEvents.Clear();

        foreach (var domainEvent in domainEvents)
            await publisher.Publish(new EventWrapper(domainEvent), cancellationToken);
    }
}