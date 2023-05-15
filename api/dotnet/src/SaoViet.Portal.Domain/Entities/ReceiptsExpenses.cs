using SaoViet.Portal.Domain.AggregateRoot;

namespace SaoViet.Portal.Domain.Entities;

public sealed class ReceiptsExpenses : AggregateRoot<ReceiptsExpensesId>
{
    public bool Type { get; set; }
    public DateTime ReceiptsExpenseDate { get; set; }
    public float Amount { get; set; }
    public string? Note { get; set; }
    public BranchId? BranchId { get; set; }

    public ReceiptsExpenses() : base(new ReceiptsExpensesId(Guid.NewGuid()))
    { }

    public ReceiptsExpenses(
        bool type,
        DateTime receiptsExpenseDate,
        float amount,
        string note,
        BranchId branchId) : base(new ReceiptsExpensesId(Guid.NewGuid()))
        => (Type, ReceiptsExpenseDate, Amount, Note, BranchId) = (type, receiptsExpenseDate, amount, note, branchId);

    public Branch? Branch { get; set; }
}

public record ReceiptsExpensesId(Guid Value);