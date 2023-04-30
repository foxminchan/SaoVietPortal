using Portal.Application.Cache;
using Portal.Domain.Primitives;
using StackExchange.Redis;

namespace Portal.Api.Extensions;

public static class RedisCacheExtension
{
    public static void AddRedisCache(
        this IServiceCollection services,
        IConfiguration config,
        Action<RedisCache>? setupAction = null)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        if (services.Contains(ServiceDescriptor.Singleton<IRedisCacheService, RedisCacheService>())) return;

        var redisCacheOption = new RedisCache();
        var redisCacheSection = config.GetSection(nameof(RedisCache));

        redisCacheSection.Bind(redisCacheOption);
        services.Configure<RedisCache>(redisCacheSection);
        setupAction?.Invoke(redisCacheOption);

        services.AddStackExchangeRedisCache(options =>
        {
            options.ConfigurationOptions = GetRedisConfigurationOptions(redisCacheOption);
            options.InstanceName = config[redisCacheOption.Prefix];
        });

        services.AddSingleton<IRedisCacheService, RedisCacheService>();
    }

    private static ConfigurationOptions GetRedisConfigurationOptions(RedisCache redisCacheOption)
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

        var endpoints = redisCacheOption.Url.ToString().Split(':');
        foreach (var endpoint in endpoints)
            configurationOptions.EndPoints.Add(endpoint);

        return configurationOptions;
    }
}