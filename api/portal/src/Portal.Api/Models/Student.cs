using System.Text.Json;

namespace Portal.Api.Models;

/// <summary>
/// Thông tin học viên
/// </summary>
public class Student
{
    /// <summary>
    /// Mã học viên
    /// </summary>
    public string? studentId { get; set; }

    /// <summary>
    /// Họ tên học viên
    /// </summary>
    /// <example>Nguyễn Văn A</example>
    public string? fullname { get; set; }

    /// <summary>
    /// Giới tính
    /// </summary>
    /// <example>true</example>
    public bool gender { get; set; }

    /// <summary>
    /// Địa chỉ
    /// </summary>
    /// <example>Nam Kỳ Khởi Nghĩa, Phường Võ Thị Sáu, Quận 3, TP. Hồ Chí Minh</example>
    public string? address { get; set; }

    /// <summary>
    /// Ngày tháng năm sinh
    /// </summary>
    /// <example>02/08/2002</example>
    public string? dob { get; set; }

    /// <summary>
    /// Nơi sinh
    /// </summary>
    /// <example>Nha Trang, Khánh Hoà</example>
    public string? pod { get; set; }

    /// <summary>
    /// Thông tin nghề nghiệp
    /// </summary>
    public string? occupation { get; set; }

    /// <summary>
    /// Mạng xã hội
    /// </summary>
    /// <example> {"facebook": "https://www.facebook.com/FoxMinChan/", "zalo": "https://zalo.me/foxminchan"}</example>
    public JsonElement? socialNetwork { get; set; }
}