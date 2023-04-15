using Portal.Domain.Enum;

namespace Portal.Domain.Entities;

public class StudentProgress
{
    public Guid progressId { get; set; }
    public string? lessonName { get; set; }
    public string? lessonContent { get; set; }
    public string? lessonDate { get; set; }
    public StudentProgressStatus progressStatus { get; set; }
    public int lessonRating { get; set; }
    public string? staffId { get; set; }
    public Staff? staff { get; set; }
    public string? studentId { get; set; }
    public string? classId { get; set; }
    public CourseEnrollment? courseEnrollment { get; set; }
}