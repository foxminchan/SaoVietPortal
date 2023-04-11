namespace Portal.Api.Models;

/// <summary>
/// Thông tin chức vụ
/// </summary>
public class Position
{
    /// <summary>
    /// Mã chức vụ
    /// </summary>
    public int positionId { get; set; }

    /// <summary>
    /// Tên chức vụ
    /// </summary>
    /// <example>Giáo viên</example>
    public string? positionName { get; set; }
}