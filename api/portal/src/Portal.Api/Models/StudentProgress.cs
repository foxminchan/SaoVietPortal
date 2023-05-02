namespace Portal.Api.Models;

public record StudentProgress(
    Guid Id,
    string LessonName,
    string LessonContent,
    string LessonDate,
    string Status,
    int LessonRating,
    string StaffId,
    string StudentId,
    string ClassId)
{
    public StudentProgress() : this(Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty, string.Empty) { }
}