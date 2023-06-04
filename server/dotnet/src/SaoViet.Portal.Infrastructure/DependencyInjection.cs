using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SaoViet.Portal.Infrastructure.Auth;
using SaoViet.Portal.Infrastructure.Cache;
using SaoViet.Portal.Infrastructure.CQRS;
using SaoViet.Portal.Infrastructure.Filters;
using SaoViet.Portal.Infrastructure.Health;
using SaoViet.Portal.Infrastructure.Middleware;
using SaoViet.Portal.Infrastructure.OpenTelemetry;
using SaoViet.Portal.Infrastructure.Persistence;
using SaoViet.Portal.Infrastructure.Searching;
using SaoViet.Portal.Infrastructure.Swagger;
using SaoViet.Portal.Infrastructure.Validator;
using SaoViet.Portal.Infrastructure.Versioning;
using System.IO.Compression;

namespace SaoViet.Portal.Infrastructure;

public static class DependencyInjection
{
    public static void AddServiceInfrastructure(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddControllers(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.ReturnHttpNotAcceptable = true;
                options.Filters.Add<LoggingFilter>();
            })
            .AddNewtonsoftJson()
            .AddApplicationPart(AssemblyReference.Assembly);

        services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    "application/json",
                    "application/xml",
                    "text/plain",
                    "image/png",
                    "image/jpeg"
                });
            })
            .Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal)
            .AddResponseCaching(options => options.MaximumBodySize = 1024)
            .AddRouting(options => options.LowercaseUrls = true);

        services.AddSqlServiceCollection<ApplicationDbContext>(builder.Configuration
            .GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException());

        services.AddCors(options => options
            .AddDefaultPolicy(policy => policy
                .WithOrigins("https://localhost:8000")
                .AllowAnyMethod()
                .AllowAnyHeader()));

        services.AddAuth();
        services.AddOpenApi();

        services.AddMediator()
            .AddValidators()
            .AddRepository()
            .AddAutoMapper(AssemblyReference.AppDomainAssembly)
            .AddEndpointsApiExplorer()
            .AddProblemDetails()
            .AddRedisCache(builder, builder.Configuration);

        services.AddSingleton<IDeveloperPageExceptionFilter, DeveloperPageExceptionFilter>();
    }

    public static void AddWebInfrastructure(this WebApplicationBuilder builder)
        => builder.AddApiVersioning()
            .AddHealthCheck()
            .AddLuceneSearch()
            .AddOpenTelemetry();

    public static void UseWebInfrastructure(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>()
            .UseMiddleware<TimeoutMiddleware>();

        app.UseApiVersioning()
            .UseCors()
            .UseExceptionHandler()
            .UseHttpsRedirection()
            .UseRateLimiter()
            .UseResponseCaching()
            .UseResponseCompression()
            .UseStatusCodePages()
            .UseStaticFiles();

        app.MapHealthCheck();
        app.Map("/", () => Results.Redirect("/swagger"));
        app.Map("/redoc", () => Results.Redirect("/api-docs"));
        app.Map("/error", () => Results.Problem("An unexpected error occurred.", statusCode: 500))
            .ExcludeFromDescription();
    }
}