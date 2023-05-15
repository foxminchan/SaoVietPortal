using SaoViet.Portal.Domain.AggregateRoot;

namespace SaoViet.Portal.Domain.Entities;

public sealed class Curriculum : AggregateRoot<CurriculumId>
{
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public CourseId? CourseId { get; set; }
    public ClassId? ClassId { get; set; }

    public Curriculum() : base(new CurriculumId(0))
    { }

    public Curriculum(
        CurriculumId id,
        TimeOnly startTime,
        TimeOnly endTime,
        CourseId courseId,
        ClassId classId) : base(id)
        => (StartTime, EndTime, CourseId, ClassId)
            = (startTime, endTime, courseId, classId);

    public Course? Course { get; set; }
    public Class? Class { get; set; }
}

public record CurriculumId(int Value);