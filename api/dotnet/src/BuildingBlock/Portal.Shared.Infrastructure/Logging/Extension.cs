using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Portal.Shared.Infrastructure.Logging.Serilog;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.SystemConsole.Themes;

namespace Portal.Shared.Infrastructure.Logging;

public static class Extension
{
    public static void AddSerilog(this WebApplicationBuilder builder, string sectionName = "Serilog")
    {
        var serilogOptions = new SerilogOptions();
        builder.Configuration.GetSection(sectionName).Bind(serilogOptions);

        builder.Host.UseSerilog((context, loggerConfiguration) =>
        {
            loggerConfiguration.ReadFrom.Configuration(context.Configuration, sectionName: sectionName);

            loggerConfiguration
                .WriteTo.File("Logs/log-.log", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
                .Enrich.WithProperty("Application", builder.Environment.ApplicationName)
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails();

            if (serilogOptions.UseConsole)
                loggerConfiguration.WriteTo.Async(writeTo =>
                    writeTo.Console(outputTemplate: serilogOptions.LogTemplate, theme: AnsiConsoleTheme.Literate));

            loggerConfiguration.WriteTo.Elasticsearch(
                new ElasticsearchSinkOptions(serilogOptions.ElasticSearchUrl)
                {
                    AutoRegisterTemplate = true,
                    IndexFormat = builder.Environment.ApplicationName
                });

            loggerConfiguration.WriteTo.Seq(serilogOptions.SeqUrl.ToString());
        });
    }
}