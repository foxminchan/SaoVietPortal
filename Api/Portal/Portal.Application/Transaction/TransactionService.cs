using Portal.Infrastructure;

namespace Portal.Application.Transaction
{
    /**
    * @Project ASP.NET Core
    * @Author: Nguyen Xuan Nhan
    * @Copyright (C) 2023 FoxMinChan. All rights reserved
    * @License MIT
    * @Create date Mon 27 Mar 2023 00:00:00 AM +07
    */

    public class TransactionService
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
}
