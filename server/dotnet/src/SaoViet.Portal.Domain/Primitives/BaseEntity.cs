using SaoViet.Portal.Domain.Interfaces;

namespace SaoViet.Portal.Domain.Primitives;

public abstract class BaseEntity<T> : IEquatable<BaseEntity<T>>, IEntity<T> where T : notnull
{
    public T Id { get; }

    protected BaseEntity(T id) => Id = id;

    public bool Equals(BaseEntity<T>? other) => Equals(other as object);

    public override bool Equals(object? obj) => obj is BaseEntity<T> entity && Id.Equals(entity);

    public static bool operator ==(BaseEntity<T>? left, BaseEntity<T>? right) => Equals(left, right);

    public static bool operator !=(BaseEntity<T>? left, BaseEntity<T>? right) => !Equals(left, right);

    public override int GetHashCode() => Id.GetHashCode();
}