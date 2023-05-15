using SaoViet.Portal.Domain.Entities;
using SaoViet.Portal.Infrastructure.Persistence;

namespace SaoViet.Portal.Infrastructure.Repositories;

public class StudentRepository : RepositoryBase<Student, StudentId>
{
    public StudentRepository(ApplicationDbContext context) : base(context)
    {
    }
}