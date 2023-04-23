namespace Portal.Api.Models;

/// <summary>
/// Receipts and expenses information
/// </summary>
public class ReceiptsExpenses
{
    /// <summary>
    /// Receipts and expenses ID
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Type of receipts and expenses
    /// </summary>
    /// <example>true</example>
    public bool Type { get; set; }

    /// <summary>
    /// Date of receipts and expenses
    /// </summary>
    /// <example>04/03/2023</example>
    public string Date { get; set; } = string.Empty;

    /// <summary>
    /// Amount of receipts and expenses
    /// </summary>
    /// <example>650000</example>
    public float Amount { get; set; }

    /// <summary>
    /// Receipts and expenses note
    /// </summary>
    /// <example>Đóng tiền internet cho trung tâm</example>
    public string Note { get; set; } = string.Empty;

    /// <summary>
    /// Branch ID
    /// </summary>
    public string BranchId { get; set; } = string.Empty;
}