using System.Diagnostics;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;

namespace SaoViet.Portal.Infrastructure.Validator;

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

    [DebuggerStepThrough]
    public static IServiceCollection AddValidators(this IServiceCollection services)
        => services.Scan(scan => scan
                .FromAssemblies(AssemblyReference.Assembly)
                .AddClasses(c => c.AssignableTo(typeof(IValidator<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
}