using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace SaoViet.Portal.Infrastructure.Auth;

public static class AuthExtension
{
    public static void AddAuth(this IServiceCollection services)
    {
        var authenticationBuilder = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

        authenticationBuilder.AddJwtBearer();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Developer", policy => policy
                .RequireClaim("Technical", "Developer")
                .RequireAuthenticatedUser());

            options.AddPolicy("Admin", policy => policy
                .RequireRole("Staff")
                .RequireClaim("Branch Manager")
                .RequireClaim("Teacher")
                .RequireClaim("Accountant")
                .RequireClaim("System Admin")
                .RequireAuthenticatedUser());

            options.AddPolicy("Teacher", policy => policy
                .RequireRole("Staff")
                .RequireClaim("Teacher")
                .RequireAuthenticatedUser());

            options.AddPolicy("Branch Manager", policy => policy
                .RequireRole("Staff")
                .RequireClaim("Branch Manager")
                .RequireClaim("Teacher")
                .RequireClaim("Accountant")
                .RequireAuthenticatedUser());

            options.AddPolicy("Accountant", policy => policy
                .RequireRole("Staff")
                .RequireClaim("Teacher")
                .RequireClaim("Accountant")
                .RequireAuthenticatedUser());

            options.AddPolicy("Student", policy => policy
                .RequireRole("Student")
                .RequireAuthenticatedUser());

            options.InvokeHandlersAfterFailure = false;

            options.FallbackPolicy = null;
        });
    }
}