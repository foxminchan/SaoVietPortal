using Newtonsoft.Json;
using SaoViet.Portal.Domain.Interfaces;
using System.Reflection;
using System.Text.Json.Serialization;

namespace SaoViet.Portal.Infrastructure.Outbox;

public sealed class OutboxEntity
{
    [JsonInclude]
    public Guid Id { get; private set; }

    [JsonInclude]
    public string? Type { get; private set; }

    [JsonInclude]
    public string? Data { get; private set; }

    [JsonInclude]
    public DateTime OccurredOn { get; private set; }

    [JsonInclude]
    public DateTime? ProcessedDate { get; set; }

    [JsonInclude]
    public string? Error { get; private set; }

    public OutboxEntity()
    { }

    public OutboxEntity(
        IDomainEvent domainEvent,
        DateTime? processedDate,
        string? error)
    {
        Id = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
        Type = domainEvent.GetType().FullName;
        Data = JsonConvert.SerializeObject(domainEvent, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        });
        ProcessedDate = processedDate;
        Error = error;
    }

    public IDomainEvent? RecreateMessage(Assembly assembly) => JsonConvert
        .DeserializeObject(
            Data ?? throw new InvalidOperationException(nameof(Data)),
            assembly.GetType(Type ?? throw new InvalidOperationException(nameof(Type)))!) as IDomainEvent;
}