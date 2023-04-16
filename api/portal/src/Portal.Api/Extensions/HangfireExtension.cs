using Hangfire;
using Hangfire.Storage.SQLite;

namespace Portal.Api.Extensions;

public static class HangfireExtension
{
    public static void AddHangfire(this WebApplicationBuilder builder)
    {
        builder.Services.AddHangfire(configuration => configuration
                   .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                   .UseColouredConsoleLogProvider()
                   .UseSimpleAssemblyNameTypeSerializer()
                   .UseRecommendedSerializerSettings()
                   .UseSQLiteStorage(builder.Configuration.GetConnectionString("SQLite")
                                     ?? throw new InvalidOperationException()));
        builder.Services.AddHangfireServer();
    }
}