using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace SaoViet.Portal.Infrastructure.Health.HealthCheck;

public class HealthCheck : IHealthCheck
{
    private readonly HealthService _healthService;

    public HealthCheck(HealthService healthService) => _healthService = healthService;

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = default)
        => _healthService.IsHealthy
            ? Task.FromResult(HealthCheckResult.Healthy("System is in a healthy state."))
            : Task.FromResult(HealthCheckResult.Unhealthy("System is in an unhealthy state."));
}