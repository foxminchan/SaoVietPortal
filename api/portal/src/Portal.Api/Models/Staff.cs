namespace Portal.Api.Models;

/// <summary>
/// Thông tin nhân viên
/// </summary>
public class Staff
{
    public string? staffId { get; set; }

    /// <example>Nguyễn Đình Ánh</example>
    public string? fullname { get; set; }

    /// <example>16/01/1989</example>
    public string? dob { get; set; }

    /// <example>Tân Phong, Biên Hoà, Đồng Nai</example>
    public string? address { get; set; }

    /// <summary>
    /// Date start work
    /// </summary>
    public string? dsw { get; set; }

    public int? positionId { get; set; }

    public string? branchId { get; set; }

    /// <summary>
    /// Manager id
    /// </summary>
    public string? managerId { get; set; }
}