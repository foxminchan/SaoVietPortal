using MediatR;
using Portal.Shared.Core.DomainEvents;
using Portal.Shared.Core.Interfaces;

namespace Portal.Shared.Core.Primitives;

public static class AggregateRootExtensions
{
    public static async Task RelayAndPublishEvents(
        this IAggregateRoot? aggregateRoot,
        IPublisher publisher,
        CancellationToken cancellationToken = default)
    {
        if (aggregateRoot?.DomainEvents is not null)
        {
            var events = new IDomainEvent[aggregateRoot.DomainEvents.Count];
            aggregateRoot.DomainEvents.CopyTo(events);
            aggregateRoot.DomainEvents.Clear();

            foreach (var @event in events)
                await publisher.Publish(new EventWrapper(@event), cancellationToken);
        }
    }
}