using FluentValidation;
using Portal.Api.Models;
using Portal.Domain.Interfaces.Common;

namespace Portal.Api.Validations;

public class UserValidator : AbstractValidator<UserResponse>
{
    public UserValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.StudentId)
            .Must((_, studentId) => studentId is null
                                     || unitOfWork.StudentRepository
                                         .TryGetStudentById(studentId, out var _))
            .WithMessage("Student with id {PropertyValue} does not exist");
        RuleFor(x => x.StaffId)
            .Must((_, staffId) => staffId is null
                                  || unitOfWork.StaffRepository
                                      .TryGetStaffById(staffId, out var _))
            .WithMessage("Staff with id {PropertyValue} does not exist");
    }
}