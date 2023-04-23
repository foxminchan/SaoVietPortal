namespace Portal.Domain.Entities;

public class ReceiptsExpenses
{
    public Guid Id { get; set; }
    public bool Type { get; set; }
    public string Date { get; set; } = string.Empty;
    public float Amount { get; set; }
    public string Note { get; set; } = string.Empty;
    public string BranchId { get; set; } = string.Empty;
    public Branch? Branch { get; set; }
}