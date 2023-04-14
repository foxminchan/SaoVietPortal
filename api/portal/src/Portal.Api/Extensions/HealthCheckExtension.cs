using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Portal.Application.Health;
using Portal.Infrastructure;

namespace Portal.Api.Extensions;

public static class HealthCheckExtension
{
    public static void AddHealthCheck(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            .AddCheck<HealthCheck>(nameof(HealthCheck), tags: new[] { "api" })
            .AddDbContextCheck<ApplicationDbContext>(tags: new[] { "db context" })
            .AddRedis("localhost:6379", tags: new[] { "redis" })
            .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
                          ?? throw new InvalidOperationException(), tags: new[] { "database" });

        builder.Services
            .AddHealthChecksUI(options =>
            {
                options.AddHealthCheckEndpoint("Health Check API", "/hc");
                options.SetEvaluationTimeInSeconds(10);
            })
            .AddInMemoryStorage();
    }

    public static IApplicationBuilder MapHealthCheck(this WebApplication app)
    {
        app.MapHealthChecks("/hc", new HealthCheckOptions()
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
            AllowCachingResponses = false,
            ResultStatusCodes =
            {
                [HealthStatus.Healthy] = StatusCodes.Status200OK,
                [HealthStatus.Degraded] = StatusCodes.Status200OK,
                [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
            }
        });
        app.MapHealthChecksUI(options => options.UIPath = "/hc-ui");

        return app;
    }
}