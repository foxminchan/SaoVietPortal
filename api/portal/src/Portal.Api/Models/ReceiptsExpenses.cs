namespace Portal.Api.Models;

/// <summary>
/// Receipts and expenses information
/// </summary>
public class ReceiptsExpenses
{
    /// <summary>
    /// Receipts and expenses ID
    /// </summary>
    public Guid receiptExpenseId { get; set; }

    /// <summary>
    /// Type of receipts and expenses
    /// </summary>
    /// <example>true</example>
    public bool type { get; set; }

    /// <summary>
    /// Date of receipts and expenses
    /// </summary>
    /// <example>04/03/2023</example>
    public string? date { get; set; }

    /// <summary>
    /// Amount of receipts and expenses
    /// </summary>
    /// <example>650000</example>
    public float amount { get; set; }

    /// <summary>
    /// Receipts and expenses note
    /// </summary>
    /// <example>Tiền mạng cho trung tâm</example>
    public string? note { get; set; }

    /// <summary>
    /// Branch ID
    /// </summary>
    public string? branchId { get; set; }
}