using SaoViet.Portal.Domain.AggregateRoot;
using SaoViet.Portal.Domain.Enums;

namespace SaoViet.Portal.Domain.Entities;

public sealed class StudentProgress : AggregateRoot<StudentProgressId>
{
    public string? LessonName { get; set; }
    public string? LessonContent { get; set; }
    public DateOnly LessonDate { get; set; }
    public StudentProgressStatus? Status { get; set; }
    public byte LessonRating { get; set; }
    public StaffId? StaffId { get; set; }
    public CourseEnrollmentId? CourseEnrollmentId { get; set; }

    public StudentProgress()
        : base(new StudentProgressId(Guid.NewGuid()))
    { }

    public StudentProgress(
        string lessonName,
        string lessonContent,
        StudentProgressStatus status,
        byte lessonRating,
        StaffId staffId,
        CourseEnrollmentId courseEnrollmentId)
        : base(new StudentProgressId(Guid.NewGuid()))
        => (LessonName, LessonContent, LessonDate, Status, LessonRating, StaffId, CourseEnrollmentId)
            = (lessonName,
                lessonContent,
                DateOnly.FromDateTime(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)),
                status,
                lessonRating,
                staffId,
                courseEnrollmentId);

    public Staff? Staff { get; set; }
    public CourseEnrollment? CourseEnrollment { get; set; }
}

public record StudentProgressId(Guid Value);