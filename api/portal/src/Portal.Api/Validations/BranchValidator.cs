using FluentValidation;
using Portal.Api.Models;

namespace Portal.Api.Validations;

public class BranchValidator : AbstractValidator<BranchResponse>
{
    public BranchValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("StudentId is required")
            .Length(8).WithMessage("StudentId must be 8 characters");
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("BranchName is required");
        RuleFor(x => x.Address)
            .MaximumLength(80).WithMessage("Address must not exceed 80 characters");
        RuleFor(x => x.Phone)
            .Matches(@"^(\+84|0)\d{9,10}$")
            .WithMessage("Phone number is not valid");
    }
}