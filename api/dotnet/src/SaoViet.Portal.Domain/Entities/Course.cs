using SaoViet.Portal.Domain.AggregateRoot;

namespace SaoViet.Portal.Domain.Entities;

public sealed class Course : AggregateRoot<CourseId>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public CourseTypeId? CourseTypeId { get; set; }

    public Course() : base(new CourseId(string.Empty))
    { }

    public Course(
        CourseId id,
        string name,
        string? description,
        CourseTypeId courseTypeId) : base(id)
        => (Name, Description, CourseTypeId)
            = (name, description, courseTypeId);

    public CourseType? CourseType { get; set; }
    public HashSet<Class> Classes { get; private set; } = new();
    public List<Curriculum> Curricula { get; private set; } = new();
}

public record CourseId(string Value);