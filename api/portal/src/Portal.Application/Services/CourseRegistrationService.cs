using Portal.Domain.Entities;
using Portal.Domain.Interfaces.Common;

namespace Portal.Application.Services;

public class CourseRegistrationService
{
    private readonly IUnitOfWork _unitOfWork;

    public CourseRegistrationService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public void AddCourseRegistration(CourseRegistration courseRegistration) => _unitOfWork.courseRegistrationRepository.AddCourseRegistration(courseRegistration);

    public void UpdateCourseRegistration(CourseRegistration courseRegistration) => _unitOfWork.courseRegistrationRepository.UpdateCourseRegistration(courseRegistration);

    public void DeleteCourseRegistration(Guid id) => _unitOfWork.courseRegistrationRepository.DeleteCourseRegistration(id);

    public CourseRegistration? GetCourseRegistrationById(Guid? id) => _unitOfWork.courseRegistrationRepository.GetCourseRegistrationById(id);

    public IEnumerable<CourseRegistration> GetAllCourseRegistrations() => _unitOfWork.courseRegistrationRepository.GetAllCourseRegistrations();
}