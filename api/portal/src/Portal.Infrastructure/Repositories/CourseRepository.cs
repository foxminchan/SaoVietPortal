using Portal.Domain.Entities;
using Portal.Domain.Interfaces;
using Portal.Infrastructure.Repositories.Common;

namespace Portal.Infrastructure.Repositories;

public class CourseRepository : RepositoryBase<Course>, ICourseRepository
{
    public CourseRepository(ApplicationDbContext context) : base(context) { }

    public void AddCourse(Course course) => Insert(course);

    public void UpdateCourse(Course course) => Update(course);

    public void DeleteCourse(string id) => Delete(x => x.courseId == id);

    public bool TryGetCourseById(string id, out Course? course) => TryGetById(id, out course);

    public IEnumerable<Course> GetAllCourses() => GetAll();
}