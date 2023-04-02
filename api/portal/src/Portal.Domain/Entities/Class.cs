namespace Portal.Domain.Entities;

public class Class
{
    public string? classId { get; set; }
    public string? startDate { get; set; }
    public string? endDate { get; set; }
    public float? fee { get; set; }
    public string? courseId { get; set; }
    public Course? course { get; set; }
    public string? branchId { get; set; }
    public Branch? branch { get; set; }
    public List<CourseEnrollment>? courseEnrollments { get; set; }
}