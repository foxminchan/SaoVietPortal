namespace Portal.Api.Models;

/// <summary>
/// Thông tin khoá học
/// </summary>
public class Course
{
    /// <summary>
    /// Mã khoá học
    /// </summary>
    public string? courseId { get; set; }

    /// <summary>
    /// Tên khoá học
    /// </summary>
    /// <example>Tin học văn phòng</example>
    public string? courseName { get; set; }

    /// <summary>
    /// Mô tả khoá học
    /// </summary>
    public string? description { get; set; }
}