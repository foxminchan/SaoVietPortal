namespace Portal.Domain.Entities;

public class CourseEnrollment
{
    public string Id { get; set; } = string.Empty;
    public Student? Student { get; set; }
    public string ClassId { get; set; } = string.Empty;
    public Class? Class { get; set; }
    public ICollection<StudentProgress>? StudentProgresses { get; private set; } = new List<StudentProgress>();
    public ICollection<CourseRegistration>? CourseRegistrations { get; private set; } = new List<CourseRegistration>();
}