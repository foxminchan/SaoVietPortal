namespace Portal.Api.Models;

/// <summary>
/// Branch information
/// </summary>
public class Branch
{
    /// <summary>
    /// Branch ID
    /// </summary>
    public string? branchId { get; set; }

    /// <summary>
    /// Name of branch
    /// </summary>
    /// <example>Tân Mai Biên Hoà</example>
    public string? branchName { get; set; }

    /// <summary>
    /// Branch address
    /// </summary>
    /// <example>Số 46B/3, KP 2, Phường Tân Mai, Tp Biên Hòa, Đồng Nai</example>
    public string? address { get; set; }

    /// <summary>
    /// Hotline
    /// </summary>
    /// <example>0931144858</example>
    public string? phone { get; set; }
}