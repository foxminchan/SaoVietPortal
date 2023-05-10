using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portal.Shared.Infrastructure.Cache.Redis;
using Portal.Shared.Infrastructure.Cache.Redis.Internals;
using StackExchange.Redis;

namespace Portal.Shared.Infrastructure.Cache;

public static class Extension
{
    public static void AddRedisCache(
        this IServiceCollection services,
        IConfiguration config,
        Action<RedisCacheOption>? setupAction = null)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        if (services.Contains(ServiceDescriptor.Singleton<IRedisCacheService, RedisCacheService>())) return;

        var redisCacheOption = new RedisCacheOption();
        var redisCacheSection = config.GetSection(nameof(RedisCacheOption));

        redisCacheSection.Bind(redisCacheOption);
        services.Configure<RedisCacheOption>(redisCacheSection);
        setupAction?.Invoke(redisCacheOption);

        services.AddStackExchangeRedisCache(options =>
        {
            options.ConfigurationOptions = GetRedisConfigurationOptions(redisCacheOption);
            options.InstanceName = config[redisCacheOption.Prefix];
        });

        services.AddSingleton<IRedisCacheService, RedisCacheService>();
    }

    private static ConfigurationOptions GetRedisConfigurationOptions(RedisCacheOption redisCacheOption)
    {
        var configurationOptions = new ConfigurationOptions
        {
            ConnectTimeout = redisCacheOption.ConnectTimeout,
            SyncTimeout = redisCacheOption.SyncTimeout,
            ConnectRetry = redisCacheOption.ConnectRetry,
            AbortOnConnectFail = redisCacheOption.AbortOnConnectFail,
            ReconnectRetryPolicy = new ExponentialRetry(redisCacheOption.DeltaBackOff),
            KeepAlive = 5,
            Ssl = redisCacheOption.Ssl,
        };

        if (!string.IsNullOrEmpty(redisCacheOption.Password))
            configurationOptions.Password = redisCacheOption.Password;

        var endpoints = redisCacheOption.Url.Split(':');
        foreach (var endpoint in endpoints)
            configurationOptions.EndPoints.Add(endpoint);

        return configurationOptions;
    }
}