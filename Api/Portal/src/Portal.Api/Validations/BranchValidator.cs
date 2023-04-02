using FluentValidation;
using Portal.Domain.Entities;

namespace Portal.Api.Validations;

public class BranchValidator : AbstractValidator<Branch>
{
    public BranchValidator()
    {
        RuleFor(x => x.branchId)
            .NotEmpty()
            .WithMessage("BranchId is required");
        RuleFor(x => x.branchName)
            .NotEmpty()
            .WithMessage("BranchName is required");
        RuleFor(x => x.address)
            .MaximumLength(80).WithMessage("Address must not exceed 80 characters");
        RuleFor(x => x.phone)
            .Matches(@"^(\+84|0)\d{9,10}$")
            .WithMessage("Phone number is not valid");
    }
}