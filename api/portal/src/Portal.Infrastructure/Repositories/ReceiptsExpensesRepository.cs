using Portal.Domain.Entities;
using Portal.Domain.Interfaces;
using Portal.Infrastructure.Repositories.Common;

namespace Portal.Infrastructure.Repositories;

public class ReceiptsExpensesRepository : RepositoryBase<ReceiptsExpenses>, IReceiptsExpensesRepository
{
    public ReceiptsExpensesRepository(ApplicationDbContext context) : base(context) { }

    public void AddReceiptsExpenses(ReceiptsExpenses receiptsExpenses) => Insert(receiptsExpenses);

    public void UpdateReceiptsExpenses(ReceiptsExpenses receiptsExpenses) => Update(receiptsExpenses);

    public void DeleteReceiptsExpenses(Guid receiptExpenseId) => Delete(x => x.receiptExpenseId == receiptExpenseId);

    public ReceiptsExpenses? GetReceiptsExpensesById(Guid? receiptExpenseId) => GetById(receiptExpenseId);

    public IEnumerable<ReceiptsExpenses> GetReceiptsExpenses() => GetAll();
}