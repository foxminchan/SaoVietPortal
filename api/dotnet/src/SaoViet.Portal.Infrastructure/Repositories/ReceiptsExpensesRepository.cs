using SaoViet.Portal.Domain.Entities;
using SaoViet.Portal.Infrastructure.Persistence;

namespace SaoViet.Portal.Infrastructure.Repositories;

public class ReceiptsExpensesRepository : RepositoryBase<ReceiptsExpenses, ReceiptsExpensesId>
{
    public ReceiptsExpensesRepository(ApplicationDbContext context) : base(context)
    {
    }
}