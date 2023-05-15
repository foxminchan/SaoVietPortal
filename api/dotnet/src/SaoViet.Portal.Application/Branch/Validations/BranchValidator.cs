using FluentValidation;
using SaoViet.Portal.Application.Branch.DTO;

namespace SaoViet.Portal.Application.Branch.Validations;

public class BranchValidator : AbstractValidator<BranchDto>
{
    public BranchValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required")
            .Length(8).WithMessage("Id must be 8 characters");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("BranchName is required");

        RuleFor(x => x.Phone)
            .Matches(@"\d{10}")
            .WithMessage("Phone number is not valid");
    }
}