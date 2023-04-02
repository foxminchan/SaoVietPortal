namespace Portal.Domain.Entities;

public class ReceiptsExpenses
{
    public Guid receiptExpenseId { get; set; }
    public bool type { get; set; }
    public string? date { get; set; }
    public float amount { get; set; }
    public string? note { get; set; }
    public string? branchId { get; set; }
    public Branch? branch { get; set; }
}