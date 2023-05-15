using SaoViet.Portal.Domain.AggregateRoot;

namespace SaoViet.Portal.Domain.Entities;

public sealed class Position : AggregateRoot<PositionId>
{
    public string? Name { get; set; }

    public Position() : base(new PositionId(0))
    { }

    public Position(PositionId id, string name) : base(id)
        => Name = name;

    public HashSet<Staff> Staffs { get; private set; } = new();
}

public record PositionId(int Value);