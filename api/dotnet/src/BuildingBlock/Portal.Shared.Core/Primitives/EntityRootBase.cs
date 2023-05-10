using System.Text.Json.Serialization;
using Portal.Shared.Core.DomainEvents;
using Portal.Shared.Core.Interfaces;

namespace Portal.Shared.Core.Primitives;

public class EntityRootBase : EntityBase, IAggregateRoot
{
    [JsonIgnore]
    public HashSet<IDomainEvent> DomainEvents { get; } = new();

    protected void AddDomainEvent(IDomainEvent domainEvent)
        => DomainEvents.Add(domainEvent);

    public void RemoveDomainEvent(EventBase eventItem)
        => DomainEvents.Remove(eventItem);
}