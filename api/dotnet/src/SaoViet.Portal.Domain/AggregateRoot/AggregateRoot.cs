using SaoViet.Portal.Domain.Interfaces;
using SaoViet.Portal.Domain.Primitives;

namespace SaoViet.Portal.Domain.AggregateRoot;

public abstract class AggregateRoot<T> : BaseEntity<T>, IAggregateRoot<T> where T : notnull
{
    protected AggregateRoot(T id) : base(id)
    {
    }

    public HashSet<IDomainEvent> DomainEvents => new();
}