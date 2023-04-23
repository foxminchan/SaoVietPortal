namespace Portal.Api.Models;

/// <summary>
/// Student progress information
/// </summary>
public class StudentProgress
{
    /// <summary>
    /// Student progress ID
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Lesson name
    /// </summary>
    /// <example>Word cơ bản</example>
    public string LessonName { get; set; } = string.Empty;

    /// <summary>
    /// Lesson content
    /// </summary>
    /// <example>Giới thiệu về Word</example>
    public string LessonContent { get; set; } = string.Empty;

    /// <summary>
    /// Lesson date
    /// </summary>
    /// <example>01/04/2023</example>
    public string LessonDate { get; set; } = string.Empty;

    /// <summary>
    /// Student progress status
    /// </summary>
    /// <example>Miễn học</example>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Lesson rating
    /// </summary>
    /// <example>10</example>
    public int LessonRating { get; set; }

    /// <summary>
    /// Teacher ID
    /// </summary>
    public string StaffId { get; set; } = string.Empty;

    /// <summary>
    /// Student ID
    /// </summary>
    public string StudentId { get; set; } = string.Empty;

    /// <summary>
    /// Class ID
    /// </summary>
    public string ClassId { get; set; } = string.Empty;
}