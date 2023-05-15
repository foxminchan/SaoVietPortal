using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SaoViet.Portal.Infrastructure.Persistence;

namespace SaoViet.Portal.Infrastructure.Health;

public static class Extension
{
    public static void AddHealthCheck(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            .AddCheck<HealthCheck.HealthCheck>(nameof(HealthCheck.HealthCheck), tags: new[] { "api" })
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

    public static void MapHealthCheck(this WebApplication app)
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
    }
}