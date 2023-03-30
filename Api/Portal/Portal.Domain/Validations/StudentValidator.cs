﻿using System.Globalization;
using FluentValidation;
using Portal.Domain.Entities;

namespace Portal.Domain.Validations;

public class StudentValidator : AbstractValidator<Student>
{
    public StudentValidator()
    {
        RuleFor(x => x.studentId)
            .NotEmpty().WithMessage("StudentId is required")
            .MaximumLength(10).WithMessage("StudentId must not exceed 10 characters");
        RuleFor(x => x.fullname)
            .NotEmpty().WithMessage("Fullname is required")
            .MaximumLength(50).WithMessage("Fullname must not exceed 50 characters");
        RuleFor(x => x.address)
            .MaximumLength(80).WithMessage("Address must not exceed 80 characters");
        RuleFor(x => x.dob)
            .Must(dobString => {
                if (!DateTime.TryParseExact(dobString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dob))
                    return false;
                return dob < DateTime.Now;
            }).WithMessage("Date of birth must be in the past");
        RuleFor(x => x.pod)
            .MaximumLength(80).WithMessage("Place of birth must not exceed 80 characters");
        RuleFor(x => x.occupation)
            .MaximumLength(80).WithMessage("Occupation must not exceed 80 characters");
    }
}