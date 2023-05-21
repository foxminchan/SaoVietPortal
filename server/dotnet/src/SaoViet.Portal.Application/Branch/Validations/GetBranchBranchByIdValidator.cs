using FluentValidation;
using SaoViet.Portal.Application.Branch.DTOs;

namespace SaoViet.Portal.Application.Branch.Validations;

public class GetBranchBranchByIdValidator : AbstractValidator<BranchDto>
{
    public GetBranchBranchByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required")
            .Length(8).WithMessage("Id must be 8 characters");
    }
}