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
                SignatureValidator = (token, _) => new JwtSecurityToken(token)
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Developer", policy => policy
                .RequireClaim("DevClaim", "developer")
                .RequireAuthenticatedUser());

            options.AddPolicy("Admin", policy => policy
                .RequireRole("Staff")
                .RequireClaim("Admin")
                .RequireAuthenticatedUser());

            options.FallbackPolicy = null;
        });

        return services;
    }
}