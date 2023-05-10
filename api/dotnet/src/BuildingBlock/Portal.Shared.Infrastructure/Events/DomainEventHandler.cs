using MediatR;
using Portal.Shared.Core.DomainEvents;
using Portal.Shared.Core.Interfaces;

namespace Portal.Shared.Infrastructure.Events;

public abstract class DomainEventHandler<TEvent> : INotificationHandler<EventWrapper>
    where TEvent : IDomainEvent
{
    protected abstract Task HandleEvent(TEvent @event, CancellationToken cancellationToken);

    public virtual async Task Handle(EventWrapper eventWrapper, CancellationToken cancellationToken)
    {
        if (eventWrapper.Event is TEvent @event)
            await HandleEvent(@event, cancellationToken);
    }
}