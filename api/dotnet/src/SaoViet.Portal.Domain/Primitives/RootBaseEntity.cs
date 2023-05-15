using System.Text.Json.Serialization;
using SaoViet.Portal.Domain.DomainEvents;
using SaoViet.Portal.Domain.Interfaces;

namespace SaoViet.Portal.Domain.Primitives;

public abstract class RootBaseEntity<T> : BaseEntity<T>, IAggregateRoot<T> where T : notnull
{
    [JsonIgnore]
    public HashSet<IDomainEvent> DomainEvents { get; private set; }

    protected RootBaseEntity(T id, IDomainEvent eventItem) : base(id)
        => DomainEvents = new HashSet<IDomainEvent> { eventItem };

    public void ClearDomainEvents(EventBase eventItem) => DomainEvents.Remove(eventItem);
}