using SaoViet.Portal.Domain.Entities;
using SaoViet.Portal.Infrastructure.Persistence;

namespace SaoViet.Portal.Infrastructure.Repositories;

public class CourseTypeRepository : RepositoryBase<CourseType, CourseTypeId>
{
    public CourseTypeRepository(ApplicationDbContext context) : base(context)
    {
    }
}