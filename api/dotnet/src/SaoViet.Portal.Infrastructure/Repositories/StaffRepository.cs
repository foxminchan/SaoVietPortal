using SaoViet.Portal.Domain.Entities;
using SaoViet.Portal.Infrastructure.Persistence;

namespace SaoViet.Portal.Infrastructure.Repositories;

public class StaffRepository : RepositoryBase<Staff, StaffId>
{
    public StaffRepository(ApplicationDbContext context) : base(context)
    {
    }
}