using FluentValidation;
using Portal.Domain.Entities;

namespace Portal.Api.Validations;

public class PaymentMethodValidator : AbstractValidator<PaymentMethod>
{
    public PaymentMethodValidator() =>
    RuleFor(x => x.paymentMethodName)
            .NotEmpty().WithMessage("Payment method name is required")
            .MaximumLength(50).WithMessage("Payment method name must not exceed 50 characters");
}