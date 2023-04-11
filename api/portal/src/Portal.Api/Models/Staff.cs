namespace Portal.Api.Models;

/// <summary>
/// Thông tin nhân viên
/// </summary>
public class Staff
{
    /// <summary>
    /// Mã nhân viên
    /// </summary>
    public string? staffId { get; set; }

    /// <summary>
    /// Họ tên
    /// </summary>
    public string? fullname { get; set; }

    /// <summary>
    /// Ngày sinh
    /// </summary>
    public string? dob { get; set; }

    /// <summary>
    /// Địa chỉ
    /// </summary>
    public string? address { get; set; }

    /// <summary>
    /// Ngày vào làm
    /// </summary>
    public string? dsw { get; set; }

    /// <summary>
    /// Mã chức vụ
    /// </summary>
    public int positionId { get; set; }

    /// <summary>
    /// Mã chi nhánh
    /// </summary>
    public string? branchId { get; set; }

    /// <summary>
    /// Mã nhân viên quản lý
    /// </summary>
    public string? managerId { get; set; }
}