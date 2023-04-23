using System.Globalization;
using FluentValidation;
using Portal.Api.Models;
using Portal.Domain.Interfaces.Common;

namespace Portal.Api.Validations;

public class ClassValidator : AbstractValidator<Class>
{
    public ClassValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Class id is required")
            .Length(10).WithMessage("Class id must be 10 characters");
        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date is required")
            .Must(startDate => DateTime.TryParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            .WithMessage("Start date is not valid");
        RuleFor(x => x.EndDate)
            .Must((classObj, endDate) => DateTime.TryParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedEndDate)
                                         && DateTime.TryParseExact(classObj.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedStartDate)
                                         && parsedEndDate > parsedStartDate)
            .WithMessage("End date must be greater than start date");
        RuleFor(x => x.Fee)
                .GreaterThan(0).WithMessage("Fee must be greater than 0");
        RuleFor(x => x.CourseId)
            .Must((_, courseId) => courseId is null
                                    || unitOfWork.CourseRepository.TryGetCourseById(courseId, out var _))
            .WithMessage("Course with id {PropertyValue} does not exist");
        RuleFor(x => x.BranchId)
            .Must((_, branchId) => branchId is null
                                   || unitOfWork.BranchRepository.TryGetBranchById(branchId, out var _))
            .WithMessage("Branch with id {PropertyValue} does not exist");
    }
}