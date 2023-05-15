using SaoViet.Portal.Domain.Entities;
using SaoViet.Portal.Infrastructure.Persistence;

namespace SaoViet.Portal.Infrastructure.Repositories;

public class CourseRegistrationRepository : RepositoryBase<CourseRegistration, CourseRegistrationId>
{
    public CourseRegistrationRepository(ApplicationDbContext context) : base(context)
    {
    }
}