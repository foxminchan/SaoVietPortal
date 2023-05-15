using System.Reflection;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using SaoViet.Portal.Domain.Interfaces;

namespace SaoViet.Portal.Domain.Primitives;

public sealed class OutboxEntity
{
    [JsonInclude]
    public Guid Id { get; private set; }

    [JsonInclude]
    public DateTime OccurredOn { get; private set; }

    [JsonInclude]
    public string? Type { get; private set; }

    [JsonInclude]
    public string? Data { get; private set; }

    public OutboxEntity()
    { }

    public OutboxEntity(Guid id, DateTime occurredOn, IDomainEvent domainEvent)
    {
        Id = id.Equals(Guid.Empty) ? Guid.NewGuid() : id;
        OccurredOn = occurredOn;
        Type = domainEvent.GetType().FullName;
        Data = JsonConvert.SerializeObject(domainEvent);
    }

    public IDomainEvent? RecreateMessage(Assembly assembly) => JsonConvert
        .DeserializeObject(
            Data ?? throw new InvalidOperationException(nameof(Data)),
            assembly.GetType(Type ?? throw new InvalidOperationException(nameof(Type)))!) as IDomainEvent;
}