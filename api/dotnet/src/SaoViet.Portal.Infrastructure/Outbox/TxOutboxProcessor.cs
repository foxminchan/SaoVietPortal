using Microsoft.Extensions.Logging;
using SaoViet.Portal.Infrastructure.EventBus;

namespace SaoViet.Portal.Infrastructure.Outbox;

public class TxOutboxProcessor : ITxOutboxProcessor
{
    private readonly IEventBus _eventBus;
    private readonly IEventStorage _eventStorage;
    private readonly ILogger<TxOutboxProcessor> _logger;

    public TxOutboxProcessor(IEventBus eventBus, IEventStorage eventStorage, ILogger<TxOutboxProcessor> logger)
        => (_eventBus, _eventStorage, _logger) = (eventBus, eventStorage, logger);

    public async Task HandleAsync(Type integrationAssemblyType, CancellationToken cancellationToken = new())
    {
        if (_eventStorage.Events.TryTake(out var domainEvent))
        {
            _logger.LogInformation("Publish @event: {Name} to the Message Broker.", domainEvent.GetType().Name);
            var @event = domainEvent.RecreateMessage(integrationAssemblyType.Assembly);
            await _eventBus.PublishAsync(@event, token: cancellationToken);
        }
    }
}