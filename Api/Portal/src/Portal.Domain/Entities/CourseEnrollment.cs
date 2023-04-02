namespace Portal.Domain.Entities;

public class CourseEnrollment
{
    public string? studentId { get; set; }
    public Student? student { get; set; }
    public string? classId { get; set; }
    public Class? @class { get; set; }
    public List<StudentProgress>? studentProgresses { get; set; }
    public List<CourseRegistration>? courseRegistrations { get; set; }
}