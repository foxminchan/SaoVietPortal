using SaoViet.Portal.Domain.Interfaces;

namespace SaoViet.Portal.Domain.DomainEvents;

public abstract class EventBase : IDomainEvent
{
    public string? EventTypeName => GetType().FullName;
    public DateTime CreatedAt { get; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
    public string? CorrelationId { get; set; }
    public IDictionary<string, object?> MetaData { get; } = new Dictionary<string, object?>();

    public virtual void Flatten()
    {
        MetaData.Add(nameof(EventTypeName), EventTypeName);
        MetaData.Add(nameof(CreatedAt), CreatedAt);
        MetaData.Add(nameof(CorrelationId), CorrelationId);
    }
}