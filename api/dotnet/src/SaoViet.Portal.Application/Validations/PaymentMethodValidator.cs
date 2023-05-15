//using FluentValidation;

//namespace Portal.Application.Validations;

//public class PaymentMethodValidator : AbstractValidator<PaymentMethodResponse>
//{
//    public PaymentMethodValidator()
//        => RuleFor(x => x.Name)
//            .NotEmpty().WithMessage("Payment method name is required")
//            .MaximumLength(50).WithMessage("Payment method name must not exceed 50 characters");
//}