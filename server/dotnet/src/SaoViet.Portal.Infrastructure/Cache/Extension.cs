using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SaoViet.Portal.Infrastructure.Cache.Redis;
using SaoViet.Portal.Infrastructure.Cache.Redis.Internals;
using StackExchange.Redis;

namespace SaoViet.Portal.Infrastructure.Cache;

public static class Extension
{
    public static void AddRedisCache(
        this IServiceCollection services,
        WebApplicationBuilder builder,
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
            options.ConfigurationOptions = GetRedisConfigurationOptions(redisCacheOption, builder);
            options.InstanceName = config[redisCacheOption.Prefix];
        });

        services.AddSingleton<IRedisCacheService, RedisCacheService>();
    }

    private static ConfigurationOptions GetRedisConfigurationOptions(
        RedisCache redisCacheOption,
        WebApplicationBuilder builder)
    {
        var configurationOptions = new ConfigurationOptions
        {
            ConnectTimeout = redisCacheOption.ConnectTimeout,
            SyncTimeout = redisCacheOption.SyncTimeout,
            ConnectRetry = redisCacheOption.ConnectRetry,
            AbortOnConnectFail = redisCacheOption.AbortOnConnectFail,
            ReconnectRetryPolicy = new ExponentialRetry(redisCacheOption.DeltaBackOff),
            KeepAlive = 5,
            Ssl = redisCacheOption.Ssl
        };

        if (!string.IsNullOrEmpty(redisCacheOption.Password))
            configurationOptions.Password = redisCacheOption.Password;

        redisCacheOption.Url = builder.Configuration
            .GetConnectionString("Redis") ?? throw new InvalidOperationException();

        var endpoints = redisCacheOption.Url.Split(':');
        foreach (var endpoint in endpoints)
            configurationOptions.EndPoints.Add(endpoint);

        return configurationOptions;
    }
}