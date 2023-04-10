using Portal.Domain.Entities;
using Portal.Domain.Interfaces.Common;

namespace Portal.Domain.Interfaces;

public interface ICourseRepository : IRepository<Course>
{
    public void AddCourse(Course course);
    public void UpdateCourse(Course course);
    public void DeleteCourse(string id);
    public IEnumerable<Course> GetAllCourses();
    public Course? GetCourseById(string id);
}