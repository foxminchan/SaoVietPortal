using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Portal.Shared.Infrastructure.Filters;

public static class Extension
{
    public static IServiceCollection AddFilters(this IServiceCollection services)
        => services.AddSingleton<IDeveloperPageExceptionFilter, DeveloperPageExceptionFilter>();
}