using MediatR;
using Microsoft.Extensions.Logging;
using SaoViet.Portal.Domain.DomainEvents;
using SaoViet.Portal.Domain.Primitives;

namespace SaoViet.Portal.Infrastructure.Outbox;

public class LocalDispatchedHandler : INotificationHandler<EventWrapper>
{
    private readonly IEventStorage _eventStorage;
    private readonly ILogger<LocalDispatchedHandler> _logger;

    public LocalDispatchedHandler(IEventStorage eventStorage, ILogger<LocalDispatchedHandler> logger)
        => (_eventStorage, _logger) = (eventStorage, logger);

    public Task Handle(EventWrapper eventWrapper, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Store @event: {nameof(eventWrapper.DomainEvent)} into the in-memory EventStore.");

        var outboxEntity = new OutboxEntity(Guid.NewGuid(), DateTime.UtcNow, eventWrapper.DomainEvent);

        _eventStorage.Events.Add(outboxEntity);

        return Task.CompletedTask;
    }
}