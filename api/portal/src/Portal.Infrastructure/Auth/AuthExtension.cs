using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Portal.Infrastructure.Auth;

public static class AuthExtension
{
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        var authenticationBuilder = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

        authenticationBuilder.AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.ForwardDefaultSelector = context => context.Request.Headers["X-Auth-Scheme"];
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = false,
                SignatureValidator = (token, _) => new JwtSecurityToken(token),
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Developer", policy => policy
                .RequireClaim("DevClaim", "developer")
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

        return services;
    }
}