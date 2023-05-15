using SaoViet.Portal.Domain.AggregateRoot;

namespace SaoViet.Portal.Domain.Entities;

public sealed class CourseEnrollment : AggregateRoot<CourseEnrollmentId>
{
    public DateOnly EnrollmentDate { get; set; }
    public StudentId? StudentId { get; set; }
    public ClassId? ClassId { get; set; }

    public CourseEnrollment()
        : base(new CourseEnrollmentId(Guid.NewGuid()))
    { }

    public CourseEnrollment(StudentId studentId, ClassId classId)
        : base(new CourseEnrollmentId(Guid.NewGuid()))
        => (EnrollmentDate, StudentId, ClassId)
            = (DateOnly.FromDateTime(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)), studentId, classId);

    public Student? Student { get; set; }
    public Class? Class { get; set; }
    public List<StudentProgress> StudentProgresses { get; private set; } = new();
    public List<CourseRegistration> CourseRegistrations { get; private set; } = new();
    public List<Certificate> Certificates { get; private set; } = new();
}

public record CourseEnrollmentId(Guid Value);