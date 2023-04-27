using Polly;
using Polly.Bulkhead;
using Polly.Extensions.Http;
using Portal.Application.Cache;
using Portal.Application.Transaction;
using Portal.Domain.Interfaces.Common;
using Portal.Infrastructure.Repositories.Common;

namespace Portal.Api.Extensions;

public static class PollyExtension
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
            .AddPolicyHandler(GetBulkheadPolicy());

        services.AddHttpClient<IRedisCacheService, RedisCacheService>(httpClientBuilder)
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy())
            .AddPolicyHandler(GetBulkheadPolicy());

        services.AddHttpClient<ITransactionService, TransactionService>(httpClientBuilder)
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy())
            .AddPolicyHandler(GetBulkheadPolicy());
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
        => Policy.BulkheadAsync<HttpResponseMessage>(maxParallelization: 30, maxQueuingActions: 20);
}