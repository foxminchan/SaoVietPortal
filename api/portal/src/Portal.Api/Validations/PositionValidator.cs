using FluentValidation;
using Portal.Api.Models;

namespace Portal.Api.Validations;

public class PositionValidator : AbstractValidator<PositionResponse>
{
    public PositionValidator()
        => RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Position name is required")
            .MaximumLength(50).WithMessage("Position name must not exceed 50 characters");
}