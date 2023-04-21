namespace Portal.Application.Transaction;

public interface ITransactionService
{
    public void ExecuteTransaction(Action action);
}