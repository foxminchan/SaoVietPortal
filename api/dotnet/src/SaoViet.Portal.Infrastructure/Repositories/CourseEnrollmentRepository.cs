using SaoViet.Portal.Domain.Entities;
using SaoViet.Portal.Infrastructure.Persistence;

namespace SaoViet.Portal.Infrastructure.Repositories;

public class CourseEnrollmentRepository : RepositoryBase<CourseEnrollment, CourseEnrollmentId>
{
    public CourseEnrollmentRepository(ApplicationDbContext context) : base(context)
    {
    }
}