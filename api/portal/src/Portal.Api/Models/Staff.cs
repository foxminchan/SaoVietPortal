namespace Portal.Api.Models;

/// <summary>
/// Staff information
/// </summary>
public class Staff
{
    /// <summary>
    /// Staff ID
    /// </summary>
    public string? staffId { get; set; }

    /// <summary>
    /// Staff full name
    /// </summary>
    /// <example>Nguyễn Đình Ánh</example>
    public string? fullname { get; set; }

    /// <summary>
    /// Date of birth
    /// </summary>
    /// <example>16/01/1989</example>
    public string? dob { get; set; }

    /// <summary>
    /// Staff Address
    /// </summary>
    /// <example>Tân Phong, Biên Hoà, Đồng Nai</example>
    public string? address { get; set; }

    /// <summary>
    /// Date start work
    /// </summary>
    public string? dsw { get; set; }

    /// <summary>
    /// Position ID
    /// </summary>
    public int? positionId { get; set; }

    /// <summary>
    /// Branch ID
    /// </summary>
    public string? branchId { get; set; }

    /// <summary>
    /// Manager ID
    /// </summary>
    public string? managerId { get; set; }
}