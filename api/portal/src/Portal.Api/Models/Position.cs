namespace Portal.Api.Models;

/// <summary>
/// Position information
/// </summary>
public class Position
{
    /// <summary>
    /// Position ID
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Position name
    /// </summary>
    /// <example>Giáo viên</example>
    public string Name { get; set; } = string.Empty;
}