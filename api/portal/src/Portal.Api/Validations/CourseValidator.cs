using FluentValidation;
using Portal.Api.Models;

namespace Portal.Api.Validations;

public class CourseValidator : AbstractValidator<Course>
{
    public CourseValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Course ID is required")
            .MaximumLength(10).WithMessage("Course ID must not exceed 10 characters");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Course name is required")
            .MaximumLength(50).WithMessage("Course name must not exceed 50 characters");
    }
}