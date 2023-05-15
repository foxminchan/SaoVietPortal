using SaoViet.Portal.Domain.Entities;
using SaoViet.Portal.Infrastructure.Persistence;

namespace SaoViet.Portal.Infrastructure.Repositories;

public class CourseRepository : RepositoryBase<Course, CourseId>
{
    public CourseRepository(ApplicationDbContext context) : base(context)
    {
    }
}