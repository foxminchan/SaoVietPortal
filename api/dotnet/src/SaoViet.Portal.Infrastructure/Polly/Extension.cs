using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Bulkhead;
using Polly.Extensions.Http;
using Polly.Timeout;
using SaoViet.Portal.Domain.Interfaces;
using SaoViet.Portal.Infrastructure.Cache.Redis;
using SaoViet.Portal.Infrastructure.Cache.Redis.Internals;
using SaoViet.Portal.Infrastructure.Persistence;

namespace SaoViet.Portal.Infrastructure.Polly;

public static class Extension
{
    public static void AddPollyPolicy(this IServiceCollection services)
    {
        var httpClientBuilder = new Action<HttpClient>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:8080");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestVersion = new Version(2, 0);
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        services.AddHttpClient<IUnitOfWork, UnitOfWork>(httpClientBuilder)
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy())
            .AddPolicyHandler(GetBulkheadPolicy())
            .AddPolicyHandler(GetTimeoutPolicy());

        services.AddHttpClient<IRedisCacheService, RedisCacheService>(httpClientBuilder)
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy())
            .AddPolicyHandler(GetBulkheadPolicy())
            .AddPolicyHandler(GetTimeoutPolicy());
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        => HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan
                .FromSeconds(Math.Pow(2, retryAttempt)));

    private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        => HttpPolicyExtensions
        .HandleTransientHttpError()
        .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

    private static AsyncBulkheadPolicy<HttpResponseMessage> GetBulkheadPolicy()
        => Policy.BulkheadAsync<HttpResponseMessage>(30, 20);

    private static AsyncTimeoutPolicy<HttpResponseMessage> GetTimeoutPolicy()
        => Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromMinutes(2));
}