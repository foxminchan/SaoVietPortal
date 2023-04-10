namespace Portal.Api.Models;

/// <summary>
/// Thông tin chi nhánh
/// </summary>
public class Branch
{
    /// <summary>
    /// Mã chi nhánh
    /// </summary>
    public string? branchId { get; set; }

    /// <summary>
    /// Tên chi nhánh
    /// </summary>
    /// <example>Tân Mai Biên Hoà</example>
    public string? branchName { get; set; }

    /// <summary>
    /// Địa chỉ
    /// </summary>
    /// <example>Số 46B/3, KP 2, Phường Tân Mai, Tp Biên Hòa, Đồng Nai</example>
    public string? address { get; set; }

    /// <summary>
    /// Số điện thoại
    /// </summary>
    /// <example>0931144858</example>
    public string? phone { get; set; }
}