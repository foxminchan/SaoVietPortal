using Portal.Shared.Core.Interfaces;

namespace Portal.Shared.Core.DomainEvents;

public abstract class EventBase : IDomainEvent
{
    public string? EventType => GetType().FullName;
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
    public string? CorrelationId { get; init; }
    public IDictionary<string, object> Metadata { get; } = new Dictionary<string, object>();
}