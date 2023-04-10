namespace Portal.Api.Models;

/// <summary>
/// Thông tin lớp học
/// </summary>
public class Class
{
    /// <summary>
    /// Mã lớp học
    /// </summary>
    public string? classId { get; set; }

    /// <summary>
    /// Ngày bắt đầu
    /// </summary>
    public string? startDate { get; set; }

    /// <summary>
    /// Ngày kết thúc
    /// </summary>
    public string? endDate { get; set; }

    /// <summary>
    /// Học phí
    /// </summary>
    public float? fee { get; set; }

    /// <summary>
    /// Mã khoá học
    /// </summary>
    public string? courseId { get; set; }

    /// <summary>
    /// Mã chi nhánh
    /// </summary>
    public string? branchId { get; set; }
}