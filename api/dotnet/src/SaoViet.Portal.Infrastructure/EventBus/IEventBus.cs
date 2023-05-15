using SaoViet.Portal.Domain.Interfaces;

namespace SaoViet.Portal.Infrastructure.EventBus;

public interface IEventBus
{
    Task PublishAsync<TEvent>(TEvent @event, string[]? topics = default, CancellationToken token = default)
        where TEvent : IDomainEvent?;

    Task SubscribeAsync<TEvent>(string[]? topics = default, CancellationToken token = default)
        where TEvent : IDomainEvent;
}