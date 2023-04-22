using FluentValidation;
using Portal.Api.Validations;
using Portal.Api.Models;

namespace Portal.Api.Extensions;

public static class FluentValidationExtension
{
    public static void AddFluentValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<Branch>, BranchValidator>();
        services.AddScoped<IValidator<Class>, ClassValidator>();
        services.AddScoped<IValidator<Course>, CourseValidator>();
        services.AddScoped<IValidator<CourseRegistration>, CourseRegistrationValidator>();
        services.AddScoped<IValidator<PaymentMethod>, PaymentMethodValidator>();
        services.AddScoped<IValidator<Position>, PositionValidator>();
        services.AddScoped<IValidator<ReceiptsExpenses>, ReceiptsExpensesValidator>();
        services.AddScoped<IValidator<Staff>, StaffValidator>();
        services.AddScoped<IValidator<Student>, StudentValidator>();
        services.AddScoped<IValidator<StudentProgress>, StudentProgressValidator>();
    }
}