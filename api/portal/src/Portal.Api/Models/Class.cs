namespace Portal.Api.Models;

/// <summary>
/// Class information
/// </summary>
public class Class
{
    /// <summary>
    /// Class ID
    /// </summary>
    public string? classId { get; set; }

    /// <summary>
    /// Date of starting
    /// </summary>
    /// <example>02/03/2023</example>
    public string? startDate { get; set; }

    /// <summary>
    /// Date of ending
    /// </summary>
    /// <example>15/07/2023</example>
    public string? endDate { get; set; }

    /// <summary>
    /// Class fee
    /// </summary>
    /// <example>450000</example>
    public float? fee { get; set; }

    /// <summary>
    /// Course ID
    /// </summary>
    public string? courseId { get; set; }

    /// <summary>
    /// Branch ID
    /// </summary>
    public string? branchId { get; set; }
}