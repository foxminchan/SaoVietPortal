using SaoViet.Portal.Domain.AggregateRoot;

namespace SaoViet.Portal.Domain.Entities;

public sealed class CourseType : AggregateRoot<CourseTypeId>
{
    public string? Name { get; set; }

    public CourseType() : base(new CourseTypeId(0))
    { }

    public CourseType(CourseTypeId id, string name) : base(id) => Name = name;

    public HashSet<Course> Courses { get; private set; } = new();
}

public record CourseTypeId(int Value);