using MediatR;
using Portal.Shared.Core.Interfaces;

namespace Portal.Shared.Core.DomainEvents;

public class EventWrapper : INotification
{
    public IDomainEvent Event { get; }

    public EventWrapper(IDomainEvent @event)
        => Event = @event;
}