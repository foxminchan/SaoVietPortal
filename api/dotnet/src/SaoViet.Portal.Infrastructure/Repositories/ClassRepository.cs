using SaoViet.Portal.Domain.Entities;
using SaoViet.Portal.Infrastructure.Persistence;

namespace SaoViet.Portal.Infrastructure.Repositories;

public class ClassRepository : RepositoryBase<Class, ClassId>
{
    public ClassRepository(ApplicationDbContext context) : base(context)
    {
    }
}