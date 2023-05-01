namespace Portal.Api.Models;

/// <summary>
/// User account information
/// </summary>
public class User
{
    /// <summary>
    /// Avatar image URL
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// Student ID
    /// </summary>
    public string StudentId { get; set; } = string.Empty;

    /// <summary>
    /// Staff ID
    /// </summary>
    public string StaffId { get; set; } = string.Empty;
}