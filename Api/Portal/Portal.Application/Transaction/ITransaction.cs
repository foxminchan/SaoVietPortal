namespace Portal.Application.Transaction;

public interface ITransaction
{
    void ExecuteTransaction(Action action);
}