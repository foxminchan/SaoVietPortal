using Portal.Domain.Entities;
using Portal.Domain.Interfaces.Common;

namespace Portal.Domain.Interfaces;

public interface IReceiptsExpensesRepository : IRepository<ReceiptsExpenses>
{
    public void AddReceiptsExpenses(ReceiptsExpenses receiptsExpenses);

    public void UpdateReceiptsExpenses(ReceiptsExpenses receiptsExpenses);

    public void DeleteReceiptsExpenses(Guid receiptExpenseId);

    public bool TryGetReceiptsExpenses(Guid receiptExpenseId, out ReceiptsExpenses? receiptsExpenses);

    public IEnumerable<ReceiptsExpenses> GetReceiptsExpenses();
}