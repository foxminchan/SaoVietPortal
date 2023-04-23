using System.Globalization;
using FluentValidation;
using Portal.Api.Models;

namespace Portal.Api.Validations;

public class StudentProgressValidator : AbstractValidator<StudentProgress>
{
    public StudentProgressValidator()
    {
        RuleFor(x => x.LessonName)
            .NotEmpty().WithMessage("Lesson name is required")
            .MaximumLength(80).WithMessage("Lesson name must not exceed 80 characters");
        RuleFor(x => x.LessonDate)
            .NotEmpty().WithMessage("Lesson date is required")
            .Must(lessonDate => DateTime.TryParseExact(lessonDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            .WithMessage("Lesson date is not valid");
        RuleFor(x => x.LessonRating)
            .NotEmpty().WithMessage("Lesson rating is required")
            .InclusiveBetween(0, 10).WithMessage("Lesson rating must be between 1 and 10");
        RuleFor(x => x.StudentId)
            .NotEmpty().WithMessage("Student id is required")
            .MaximumLength(10).WithMessage("Student id must not exceed 10 characters");
        RuleFor(x => x.ClassId)
            .NotEmpty().WithMessage("Class id is required")
            .MaximumLength(10).WithMessage("Class id must not exceed 10 characters");
    }
}