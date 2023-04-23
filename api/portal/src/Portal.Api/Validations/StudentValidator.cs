using FluentValidation;
using Portal.Api.Models;
using System.Globalization;

namespace Portal.Api.Validations;

public class StudentValidator : AbstractValidator<Student>
{
    public StudentValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("StudentId is required")
            .MaximumLength(10).WithMessage("StudentId must not exceed 10 characters");
        RuleFor(x => x.Fullname)
            .NotEmpty().WithMessage("Fullname is required")
            .MaximumLength(50).WithMessage("Fullname must not exceed 50 characters");
        RuleFor(x => x.Address)
            .MaximumLength(80).WithMessage("Address must not exceed 80 characters");
        RuleFor(x => x.Dob)
            .Must(dobString =>
            {
                if (!DateTime.TryParseExact(dobString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dob))
                    return false;
                return dob < DateTime.Now;
            }).WithMessage("Date of birth must be in the past")
            .NotEmpty().WithMessage("Date of birth is required");
        RuleFor(x => x.Pod)
            .MaximumLength(80).WithMessage("Place of birth must not exceed 80 characters");
        RuleFor(x => x.Occupation)
            .MaximumLength(80).WithMessage("Occupation must not exceed 80 characters");
    }
}