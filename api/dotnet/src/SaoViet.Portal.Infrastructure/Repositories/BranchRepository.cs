using SaoViet.Portal.Domain.Entities;
using SaoViet.Portal.Infrastructure.Persistence;

namespace SaoViet.Portal.Infrastructure.Repositories;

public class BranchRepository : RepositoryBase<Branch, BranchId>
{
    public BranchRepository(ApplicationDbContext context) : base(context)
    {
    }
}