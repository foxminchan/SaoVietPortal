using MediatR;
using SaoViet.Portal.Domain.DomainEvents;
using SaoViet.Portal.Domain.Interfaces;

namespace SaoViet.Portal.Infrastructure.EventHandler;

public abstract class DomainEventHandler<T> : INotificationHandler<EventWrapper>
    where T : IDomainEvent
{
    protected abstract Task HandleEvent(T domainEvent, CancellationToken cancellationToken);

    public async Task Handle(EventWrapper notification, CancellationToken cancellationToken)
    {
        if (notification.DomainEvent is T domainEvent)
            await HandleEvent(domainEvent, cancellationToken);
    }
}