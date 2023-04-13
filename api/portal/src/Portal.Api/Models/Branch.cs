namespace Portal.Api.Models;

/// <summary>
/// Branch information
/// </summary>
public class Branch
{
    public string? branchId { get; set; }

    /// <example>Tân Mai Biên Hoà</example>
    public string? branchName { get; set; }

    /// <example>Số 46B/3, KP 2, Phường Tân Mai, Tp Biên Hòa, Đồng Nai</example>
    public string? address { get; set; }

    /// <example>0931144858</example>
    public string? phone { get; set; }
}