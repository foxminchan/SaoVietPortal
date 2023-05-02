namespace Portal.Api.Models;

public record ReceiptsExpenses(Guid Id, bool Type, string Date, float Amount, string Note, string BranchId)
{
    public ReceiptsExpenses() : this(Guid.NewGuid(), false, string.Empty, 0, string.Empty, string.Empty) { }
}