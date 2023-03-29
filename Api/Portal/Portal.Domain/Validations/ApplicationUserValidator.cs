using FluentValidation;
using Portal.Domain.Entities;

namespace Portal.Domain.Validations;

public class ApplicationUserValidator : AbstractValidator<ApplicationUser>
{
    public ApplicationUserValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Email is not valid");
        RuleFor(x => x.PhoneNumber)
            .Matches(@"^(\+84|0)\d{9,10}$")
            .WithMessage("Phone number is not valid");
    }
}