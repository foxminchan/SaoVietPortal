using System.Collections.Concurrent;
using SaoViet.Portal.Domain.Primitives;

namespace SaoViet.Portal.Infrastructure.Outbox;

public interface IEventStorage
{
    public ConcurrentBag<OutboxEntity> Events { get; }
}

public class EventStorage : IEventStorage
{
    public ConcurrentBag<OutboxEntity> Events { get; } = new();
}