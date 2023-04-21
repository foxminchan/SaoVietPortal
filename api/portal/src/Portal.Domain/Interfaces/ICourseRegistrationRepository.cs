using Portal.Domain.Entities;
using Portal.Domain.Interfaces.Common;

namespace Portal.Domain.Interfaces;

public interface ICourseRegistrationRepository : IRepository<CourseRegistration>
{
    public void AddCourseRegistration(CourseRegistration courseRegistration);
    public void UpdateCourseRegistration(CourseRegistration courseRegistration);
    public void DeleteCourseRegistration(Guid id);
    public bool TryGetCourseRegistrationById(Guid id, out CourseRegistration? courseRegistration);
    public IEnumerable<CourseRegistration> GetAllCourseRegistrations();
}