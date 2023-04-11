using Portal.Domain.Entities;
using Portal.Domain.Interfaces.Common;

namespace Portal.Application.Services;

public class ReceiptsExpensesService
{
    private readonly IUnitOfWork _unitOfWork;

    public ReceiptsExpensesService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public void AddReceiptsExpenses(ReceiptsExpenses receiptsExpenses) => _unitOfWork.receiptsExpensesRepository.AddReceiptsExpenses(receiptsExpenses);

    public void UpdateReceiptsExpenses(ReceiptsExpenses receiptsExpenses) => _unitOfWork.receiptsExpensesRepository.UpdateReceiptsExpenses(receiptsExpenses);

    public void DeleteReceiptsExpenses(Guid receiptExpenseId) => _unitOfWork.receiptsExpensesRepository.DeleteReceiptsExpenses(receiptExpenseId);

    public ReceiptsExpenses? GetReceiptsExpensesById(Guid? receiptExpenseId) => _unitOfWork.receiptsExpensesRepository.GetReceiptsExpensesById(receiptExpenseId);

    public IEnumerable<ReceiptsExpenses> GetReceiptsExpenses() => _unitOfWork.receiptsExpensesRepository.GetReceiptsExpenses();
}