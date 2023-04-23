namespace Portal.Api.Models;

/// <summary>
/// Branch information
/// </summary>
public class Branch
{
    /// <summary>
    /// Branch ID
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Name of branch
    /// </summary>
    /// <example>Tân Mai Biên Hoà</example>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Branch address
    /// </summary>
    /// <example>Số 46B/3, KP 2, Phường Tân Mai, Tp Biên Hòa, Đồng Nai</example>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Hotline
    /// </summary>
    /// <example>0931144858</example>
    public string Phone { get; set; } = string.Empty;
}