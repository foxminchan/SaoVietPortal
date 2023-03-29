using Portal.Infrastructure;

namespace Portal.Application.Transaction;

public class TransactionService : ITransaction
{
    private readonly ApplicationDbContext _context;

    public TransactionService(ApplicationDbContext context) => _context = context;

    public void ExecuteTransaction(Action action)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            action();
            _context.SaveChanges();
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }
}