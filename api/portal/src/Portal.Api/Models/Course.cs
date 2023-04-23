namespace Portal.Api.Models;

/// <summary>
/// Course information
/// </summary>
public class Course
{
    /// <summary>
    /// Course ID
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Name of course
    /// </summary>
    /// <example>Tin học văn phòng</example>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Course description
    /// </summary>
    /// <example>Thực hành với các phần mềm Microsoft Office</example>
    public string Description { get; set; } = string.Empty;
}