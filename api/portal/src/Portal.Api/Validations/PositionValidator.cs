using FluentValidation;
using Portal.Api.Models;

namespace Portal.Api.Validations;

public class PositionValidator : AbstractValidator<Position>
{
    public PositionValidator()
    {
        RuleFor(x => x.positionId)
            .Must(x => x > 0).WithMessage("PositionId must be greater than 0");

        RuleFor(x => x.positionName)
            .NotEmpty().WithMessage("Position name is required")
            .MaximumLength(50).WithMessage("Position name must not exceed 50 characters");
    }
}