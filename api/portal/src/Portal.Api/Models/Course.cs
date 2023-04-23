namespace Portal.Api.Models;

/// <summary>
/// Course information
/// </summary>
public class Course
{
    /// <summary>
    /// Course ID
    /// </summary>
    public string? courseId { get; set; }

    /// <summary>
    /// Name of course
    /// </summary>
    /// <example>Tin học văn phòng</example>
    public string? courseName { get; set; }

    /// <summary>
    /// Course description
    /// </summary>
    /// <example>Thực hành với các phần mềm Microsoft Office</example>
    public string? description { get; set; }
}