using SaoViet.Portal.Domain.Entities;
using SaoViet.Portal.Infrastructure.Persistence;

namespace SaoViet.Portal.Infrastructure.Repositories;

public class StudentProgressRepository : RepositoryBase<StudentProgress, StudentProgressId>
{
    public StudentProgressRepository(ApplicationDbContext context) : base(context)
    {
    }
}