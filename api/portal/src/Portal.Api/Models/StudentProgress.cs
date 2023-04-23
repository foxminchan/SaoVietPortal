namespace Portal.Api.Models;

/// <summary>
/// Student progress information
/// </summary>
public class StudentProgress
{
    /// <summary>
    /// Student progress ID
    /// </summary>
    public Guid progressId { get; set; }

    /// <summary>
    /// Lesson name
    /// </summary>
    /// <example>Word cơ bản</example>
    public string? lessonName { get; set; }

    /// <summary>
    /// Lesson content
    /// </summary>
    /// <example>Giới thiệu về Word</example>
    public string? lessonContent { get; set; }

    /// <summary>
    /// Lesson date
    /// </summary>
    /// <example>01/04/2023</example>
    public string? lessonDate { get; set; }

    /// <summary>
    /// Student progress status
    /// </summary>
    /// <example>Miễn học</example>
    public string? progressStatus { get; set; }

    /// <summary>
    /// Lesson rating
    /// </summary>
    /// <example>10</example>
    public int lessonRating { get; set; }

    /// <summary>
    /// Teacher ID
    /// </summary>
    public string? staffId { get; set; }

    /// <summary>
    /// Student ID
    /// </summary>
    public string? studentId { get; set; }

    /// <summary>
    /// Class ID
    /// </summary>
    public string? classId { get; set; }
}