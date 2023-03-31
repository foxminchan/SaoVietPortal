using FluentValidation;
using Portal.Domain.Entities;

namespace Portal.Api.Validations;

public class PositionValidator : AbstractValidator<Position>
{
    public PositionValidator() =>
    RuleFor(x => x.positionName)
            .NotEmpty().WithMessage("Position name is required")
            .MaximumLength(50).WithMessage("Position name must not exceed 50 characters");
}