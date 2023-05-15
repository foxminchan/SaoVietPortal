using SaoViet.Portal.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SaoViet.Portal.Domain.Primitives;

public abstract class Outbox<TId> : RootBaseEntity<TId>
    where TId : notnull
{
    public new TId? Id { get; set; }
    public string? Type { get; set; }
    public string? AggregateType { get; set; }
    public Guid AggregateId { get; set; }
    public byte[]? Payload { get; set; }

    protected Outbox(TId id, IDomainEvent eventItem) : base(id, eventItem)
    {
    }

    public bool Validate()
    {
        if (Id is null)
            throw new ValidationException("Id of the Outbox entity couldn't be null.");

        if (string.IsNullOrEmpty(Type))
            throw new ValidationException("Type of the Outbox entity couldn't be null or empty.");

        if (string.IsNullOrEmpty(AggregateType))
            throw new ValidationException("AggregateType of the Outbox entity couldn't be null or empty.");

        if (Guid.Empty == AggregateId)
            throw new ValidationException("AggregateId of the Outbox entity couldn't be null.");

        if (Payload is null)
            throw new ValidationException("Payload of the Outbox entity couldn't be null");

        return true;
    }
}