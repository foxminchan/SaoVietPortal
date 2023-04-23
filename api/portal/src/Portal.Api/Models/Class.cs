namespace Portal.Api.Models;

/// <summary>
/// Class information
/// </summary>
public class Class
{
    /// <summary>
    /// Class ID
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Date of starting
    /// </summary>
    /// <example>02/03/2023</example>
    public string StartDate { get; set; } = string.Empty;

    /// <summary>
    /// Date of ending
    /// </summary>
    /// <example>15/07/2023</example>
    public string EndDate { get; set; } = string.Empty;

    /// <summary>
    /// Class fee
    /// </summary>
    /// <example>450000</example>
    public float Fee { get; set; }

    /// <summary>
    /// Course ID
    /// </summary>
    public string CourseId { get; set; } = string.Empty;

    /// <summary>
    /// Branch ID
    /// </summary>
    public string BranchId { get; set; } = string.Empty;
}