using FluentValidation;
using FluentValidation.Results;

namespace Portal.Shared.Infrastructure.Validator;

public static class Extension
{
    private static ValidationResultModel ToValidationResultModel(this ValidationResult validationResult)
        => new(validationResult);

    public static async Task HandleValidation<TRequest>(this IValidator<TRequest> validator, TRequest request)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.ToValidationResultModel());
    }
}