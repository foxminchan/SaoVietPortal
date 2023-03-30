﻿using FluentValidation;
using Portal.Domain.Entities;

namespace Portal.Domain.Validations;

public class CourseValidator : AbstractValidator<Course>
{
    public CourseValidator()
    {
        RuleFor(x => x.courseId)
            .NotEmpty().WithMessage("Course ID is required")
            .MaximumLength(10).WithMessage("Course ID must not exceed 10 characters");
        RuleFor(x => x.courseName)
            .NotEmpty().WithMessage("Course name is required")
            .MaximumLength(50).WithMessage("Course name must not exceed 50 characters");
    }
}