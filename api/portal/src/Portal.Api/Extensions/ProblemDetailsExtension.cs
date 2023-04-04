using Microsoft.AspNetCore.Diagnostics;
using Portal.Infrastructure.ErrorHandler;

namespace Portal.Api.Extensions;

public static class ProblemDetailsExtension
{
    public static IServiceCollection AddProblemDetailsDeveloperPageExceptionFilter(this IServiceCollection services) =>
        services.AddSingleton<IDeveloperPageExceptionFilter, ProblemDetailsDeveloperPageExceptionFilter>();
}