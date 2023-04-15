using System.Globalization;
using FluentValidation;
using Portal.Api.Models;

namespace Portal.Api.Validations;

public class ReceiptsExpensesValidator : AbstractValidator<ReceiptsExpenses>
{
    public ReceiptsExpensesValidator()
    {
        RuleFor(x => x.type)
            .NotEmpty().WithMessage("Type is required");
        RuleFor(x => x.date)
            .Must(dateString => DateTime.TryParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            .WithMessage("Date is not valid");
        RuleFor(x => x.amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0");
    }
}