using System.Security.Claims;
using System.Threading.RateLimiting;

namespace Portal.Api.Extensions;

public static class RateLimiterExtension
{
    private const string POLICY = "PerUser";

    public static void AddRateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            options.AddPolicy(POLICY, context =>
            {
                var username = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

                return RateLimitPartition.GetTokenBucketLimiter(username, _ => new TokenBucketRateLimiterOptions
                {
                    ReplenishmentPeriod = TimeSpan.FromSeconds(10),
                    AutoReplenishment = true,
                    TokenLimit = 100,
                    TokensPerPeriod = 100,
                    QueueLimit = 100,
                });
            });
        });
    }

    public static IEndpointConventionBuilder RequirePerUserRateLimit(this IEndpointConventionBuilder builder) => builder.RequireRateLimiting(POLICY);
}