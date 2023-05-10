using Portal.Shared.Core.Interfaces;

namespace Portal.Shared.Core.Primitives;

public class EntityBase : IEntity
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime Created { get; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
    public DateTime? Updated { get; protected set; }
}