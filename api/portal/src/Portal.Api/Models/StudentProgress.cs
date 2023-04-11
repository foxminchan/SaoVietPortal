namespace Portal.Api.Models;

/// <summary>
/// Thông tin tiến độ học tập của học viên
/// </summary>
public class StudentProgress
{
    /// <summary>
    /// Mã tiến độ học tập
    /// </summary>
    public Guid progressId { get; set; }

    /// <summary>
    /// Tên bài học
    /// </summary>
    /// <example>Word cơ bản</example>
    public string? lessonName { get; set; }

    /// <summary>
    /// Nội dung bài học
    /// </summary>
    /// <example>Giới thiệu về Word</example>
    public string? lessonContent { get; set; }

    /// <summary>
    /// Ngày học
    /// </summary>
    public string? lessonDate { get; set; }

    /// <summary>
    /// Trạng thái tiến độ học tập
    /// </summary>
    /// <example>Miễn học</example>
    public string? progressStatus { get; set; }

    /// <summary>
    /// Đánh giá bài học
    /// </summary>
    /// <example>10</example>
    public int lessonRating { get; set; }

    /// <summary>
    /// Mã giáo viên
    /// </summary>
    public string? staffId { get; set; }

    /// <summary>
    /// Mã học viên
    /// </summary>
    public string? studentId { get; set; }

    /// <summary>
    /// Mã lớp học
    /// </summary>
    public string? classId { get; set; }
}