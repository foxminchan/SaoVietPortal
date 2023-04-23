namespace Portal.Domain.Entities;

public class StudentProgress
{
    public Guid Id { get; set; }
    public string LessonName { get; set; } = string.Empty;
    public string LessonContent { get; set; } = string.Empty;
    public string LessonDate { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int LessonRating { get; set; }
    public string StaffId { get; set; } = string.Empty;
    public Staff? Staff { get; set; }
    public string StudentId { get; set; } = string.Empty;
    public string ClassId { get; set; } = string.Empty;
    public CourseEnrollment? CourseEnrollment { get; set; }
}