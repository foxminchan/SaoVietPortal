namespace Portal.Api.Models;

/// <summary>
/// Student information
/// </summary>
public class Student
{
    /// <summary>
    /// Student ID
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Student full name
    /// </summary>
    /// <example>Nguyễn Văn A</example>
    public string Fullname { get; set; } = string.Empty;

    /// <summary>
    /// Gender
    /// </summary>
    /// <example>true</example>
    public bool Gender { get; set; }

    /// <summary>
    /// Address
    /// </summary>
    /// <example>Nam Kỳ Khởi Nghĩa, Phường Võ Thị Sáu, Quận 3, TP. Hồ Chí Minh</example>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Date of birth
    /// </summary>
    /// <example>02/08/2002</example>
    public string Dob { get; set; } = string.Empty;

    /// <summary>
    /// Place of birth
    /// </summary>
    /// <example>Nha Trang, Khánh Hoà</example>
    public string Pod { get; set; } = string.Empty;

    /// <summary>
    /// Job information
    /// </summary>
    /// <example>Giảng viên</example>
    public string Occupation { get; set; } = string.Empty;

    /// <summary>
    /// Social links
    /// </summary>
    /// <example>{"facebook": "https://www.facebook.com/FoxMinChan/", "zalo": "https://zalo.me/foxminchan"}</example>
    public string? SocialNetwork { get; set; }
}