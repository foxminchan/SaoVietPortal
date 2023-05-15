namespace SaoViet.Portal.Application.DTOs;

public record StudentProgressDto(
    Guid Id,
    string LessonName,
    string LessonContent,
    string LessonDate,
    string Status,
    int LessonRating,
    string StaffId,
    string StudentId,
    string ClassId);