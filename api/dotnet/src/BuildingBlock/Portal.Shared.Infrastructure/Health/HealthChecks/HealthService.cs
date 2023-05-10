namespace Portal.Shared.Infrastructure.Health.HealthChecks;

public class HealthService
{
    public bool IsHealthy { get; private set; } = true;
}