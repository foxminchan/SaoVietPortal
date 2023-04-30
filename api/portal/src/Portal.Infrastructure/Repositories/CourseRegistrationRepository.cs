using Portal.Domain.Entities;
using Portal.Domain.Interfaces;
using Portal.Infrastructure.Repositories.Common;

namespace Portal.Infrastructure.Repositories;

public class CourseRegistrationRepository : RepositoryBase<CourseRegistration>, ICourseRegistrationRepository
{
    public CourseRegistrationRepository(ApplicationDbContext context) : base(context)
    {
    }

    public void AddCourseRegistration(CourseRegistration courseRegistration) => Insert(courseRegistration);

    public void UpdateCourseRegistration(CourseRegistration courseRegistration) => Update(courseRegistration);

    public void DeleteCourseRegistration(Guid id) => Delete(x => x.Id == id);

    public bool TryGetCourseRegistrationById(Guid id, out CourseRegistration? courseRegistration)
        => TryGetById(id, out courseRegistration);

    public IEnumerable<CourseRegistration> GetAllCourseRegistrations() => GetAll();
}