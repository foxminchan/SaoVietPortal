using Portal.Domain.Entities;
using Portal.Domain.Interfaces.Common;

namespace Portal.Application.Services;

public class CourseService
{
    private readonly IUnitOfWork _unitOfWork;

    public CourseService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public void AddCourse(Course course) => _unitOfWork.courseRepository.AddCourse(course);

    public void UpdateCourse(Course course) => _unitOfWork.courseRepository.UpdateCourse(course);

    public void DeleteCourse(string id) => _unitOfWork.courseRepository.DeleteCourse(id);

    public bool TryGetCourseById(string id, out Course? course)
        => _unitOfWork.courseRepository.TryGetCourseById(id, out course);

    public IEnumerable<Course> GetAllCourses() => _unitOfWork.courseRepository.GetAllCourses();
}