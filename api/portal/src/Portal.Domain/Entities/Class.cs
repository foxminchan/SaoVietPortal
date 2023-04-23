namespace Portal.Domain.Entities;

public class Class
{
    public string Id { get; set; } = string.Empty;
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public float Fee { get; set; }
    public string CourseId { get; set; } = string.Empty;
    public Course? Course { get; set; }
    public string BranchId { get; set; } = string.Empty;
    public Branch? Branch { get; set; }
    public ICollection<CourseEnrollment> CourseEnrollments { get; private set; } = new HashSet<CourseEnrollment>();
}