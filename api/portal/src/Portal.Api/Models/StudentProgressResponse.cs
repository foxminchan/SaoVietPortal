namespace Portal.Api.Models;

public record StudentProgressResponse(
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
    public StudentProgressResponse() : this(Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty, string.Empty) { }
}