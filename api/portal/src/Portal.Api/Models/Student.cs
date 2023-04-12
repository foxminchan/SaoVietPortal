using System.Text.Json;

namespace Portal.Api.Models;

/// <summary>
/// Student information
/// </summary>
public class Student
{
    public string? studentId { get; set; }

    /// <example>Nguyễn Văn A</example>
    public string? fullname { get; set; }

    /// <example>true</example>
    public bool gender { get; set; }

    /// <example>Nam Kỳ Khởi Nghĩa, Phường Võ Thị Sáu, Quận 3, TP. Hồ Chí Minh</example>
    public string? address { get; set; }

    /// <summary>
    /// Date of birth
    /// </summary>
    /// <example>02/08/2002</example>
    public string? dob { get; set; }

    /// <summary>
    /// Place of birth
    /// </summary>
    /// <example>Nha Trang, Khánh Hoà</example>
    public string? pod { get; set; }

    /// <summary>
    /// Job information
    /// </summary>
    public string? occupation { get; set; }

    /// <example> {"facebook": "https://www.facebook.com/FoxMinChan/", "zalo": "https://zalo.me/foxminchan"}</example>
    public JsonElement? socialNetwork { get; set; }
}