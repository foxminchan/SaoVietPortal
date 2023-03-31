using System.Globalization;
using FluentValidation;
using Portal.Domain.Entities;

namespace Portal.Api.Validations;

public class ClassValidator : AbstractValidator<Class>
{
    public ClassValidator()
    {
        RuleFor(x => x.classId)
            .NotEmpty().WithMessage("Class id is required")
            .MaximumLength(10).WithMessage("Class id must not exceed 10 characters");
        RuleFor(x => x.startDate)
            .NotEmpty().WithMessage("Start date is required")
            .Must(startDate => DateTime.TryParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedStartDate))
            .WithMessage("Start date is not valid");
        RuleFor(x => x.endDate)
            .Must((classObj, endDate) => DateTime.TryParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedEndDate)
                                         && DateTime.TryParseExact(classObj.startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedStartDate)
                                         && parsedEndDate > parsedStartDate)
            .WithMessage("End date must be greater than start date");
        RuleFor(x => x.fee)
                .GreaterThan(0).WithMessage("Fee must be greater than 0");
    }
}