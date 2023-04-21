using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Portal.Infrastructure;

namespace Portal.Application.Transaction;

public class TransactionService : ITransactionService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<TransactionService> _logger;

    public TransactionService(ApplicationDbContext context, ILogger<TransactionService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public void ExecuteTransaction(Action action)
    {
        _context.Database.CreateExecutionStrategy()
            .Execute(() =>
            {
                using var transaction = _context.Database.BeginTransaction();
                try
                {
                    action();
                    _context.SaveChanges();
                    transaction.Commit();
                    _logger.LogInformation("Transaction committed");
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    _logger.LogError(e, "Transaction rolled back");
                    throw;
                }
            });
    }
}