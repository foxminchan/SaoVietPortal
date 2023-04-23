using System.Globalization;
using FluentValidation;
using Portal.Api.Models;
using Portal.Domain.Interfaces.Common;

namespace Portal.Api.Validations;

public class ClassValidator : AbstractValidator<Class>
{
    public ClassValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.classId)
            .NotEmpty().WithMessage("Class id is required")
            .Length(10).WithMessage("Class id must be 10 characters");
        RuleFor(x => x.startDate)
            .NotEmpty().WithMessage("Start date is required")
            .Must(startDate => DateTime.TryParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            .WithMessage("Start date is not valid");
        RuleFor(x => x.endDate)
            .Must((classObj, endDate) => DateTime.TryParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedEndDate)
                                         && DateTime.TryParseExact(classObj.startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedStartDate)
                                         && parsedEndDate > parsedStartDate)
            .WithMessage("End date must be greater than start date");
        RuleFor(x => x.fee)
                .GreaterThan(0).WithMessage("Fee must be greater than 0");
        RuleFor(x => x.courseId)
            .Must((_, courseId) => courseId is null
                                    || unitOfWork.courseRepository.TryGetCourseById(courseId, out var _))
            .WithMessage("Course with id {PropertyValue} does not exist");
        RuleFor(x => x.branchId)
            .Must((_, branchId) => branchId is null
                                   || unitOfWork.branchRepository.TryGetBranchById(branchId, out var _))
            .WithMessage("Branch with id {PropertyValue} does not exist");
    }
}