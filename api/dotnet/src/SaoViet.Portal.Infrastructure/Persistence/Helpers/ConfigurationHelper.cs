using Microsoft.Extensions.Configuration;

namespace SaoViet.Portal.Infrastructure.Persistence.Helpers;

public static class ConfigurationHelper
{
    public static IConfiguration GetConfiguration(string? basePath = null)
    {
        basePath ??= Directory.GetCurrentDirectory();
        return new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
            .AddEnvironmentVariables()
            .Build();
    }
}