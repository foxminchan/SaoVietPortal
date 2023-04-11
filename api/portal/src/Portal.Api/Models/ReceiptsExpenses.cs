namespace Portal.Api.Models;

/// <summary>
/// Thông tin thu chi
/// </summary>
public class ReceiptsExpenses
{
    /// <summary>
    /// Mã phiếu thu chi
    /// </summary>
    public Guid receiptExpenseId { get; set; }

    /// <summary>
    /// Loại phiếu
    /// </summary>
    public bool type { get; set; }

    /// <summary>
    /// Ngày thu chi
    /// </summary>
    public string? date { get; set; }

    /// <summary>
    /// Số tiền
    /// </summary>
    public float amount { get; set; }

    /// <summary>
    /// Nội dung
    /// </summary>
    public string? note { get; set; }

    /// <summary>
    /// Mã chi nhánh
    /// </summary>
    public string? branchId { get; set; }
}