using Portal.Domain.Primitives;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.SystemConsole.Themes;

namespace Portal.Api.Extensions;

public static class SerilogExtension
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

            if (!string.IsNullOrEmpty(serilogOptions.ElasticSearchUrl))
                loggerConfiguration.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(serilogOptions.ElasticSearchUrl)) { AutoRegisterTemplate = true, IndexFormat = builder.Environment.ApplicationName });

            if (!string.IsNullOrEmpty(serilogOptions.SeqUrl))
                loggerConfiguration.WriteTo.Seq(serilogOptions.SeqUrl);
        });
    }
}