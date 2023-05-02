namespace Portal.Api.Models;

public record ReceiptsExpensesResponse(Guid Id, bool Type, string Date, float Amount, string Note, string BranchId)
{
    public ReceiptsExpensesResponse() : this(Guid.NewGuid(), false, string.Empty, 0, string.Empty, string.Empty) { }
}