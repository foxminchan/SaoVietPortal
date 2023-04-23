namespace Portal.Api.Models;

/// <summary>
/// Staff information
/// </summary>
public class Staff
{
    /// <summary>
    /// Staff ID
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Staff full name
    /// </summary>
    /// <example>Nguyễn Đình Ánh</example>
    public string Fullname { get; set; } = string.Empty;

    /// <summary>
    /// Date of birth
    /// </summary>
    /// <example>16/01/1989</example>
    public string Dob { get; set; } = string.Empty;

    /// <summary>
    /// Staff Address
    /// </summary>
    /// <example>Tân Phong, Biên Hoà, Đồng Nai</example>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Date start work
    /// </summary>
    /// <example>01/02/2020</example>
    public string Dsw { get; set; } = string.Empty;

    /// <summary>
    /// Position ID
    /// </summary>
    public int? PositionId { get; set; }

    /// <summary>
    /// Branch ID
    /// </summary>
    public string BranchId { get; set; } = string.Empty;

    /// <summary>
    /// Manager ID
    /// </summary>
    public string ManagerId { get; set; } = string.Empty;
}