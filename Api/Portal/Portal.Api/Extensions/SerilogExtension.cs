using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace Portal.Api.Extensions;

public static class SerilogExtension
{
    public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder, string sectionName = "Serilog")
    {
        var serilogOptions = new SerilogOptions();
        builder.Configuration.GetSection(sectionName).Bind(serilogOptions);

        builder.Host.UseSerilog((context, loggerConfiguration) =>
        {
            loggerConfiguration.ReadFrom.Configuration(context.Configuration, sectionName: sectionName);
            loggerConfiguration
                .Enrich.WithProperty("Application", builder.Environment.ApplicationName)
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails();

            if (serilogOptions.UseConsole)
                loggerConfiguration.WriteTo.Async(writeTo =>
                    writeTo.Console(outputTemplate: serilogOptions.LogTemplate));

            if (!string.IsNullOrEmpty(serilogOptions.ElasticSearchUrl))
                loggerConfiguration.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(serilogOptions.ElasticSearchUrl)) { AutoRegisterTemplate = true, IndexFormat = builder.Environment.ApplicationName });

            if (!string.IsNullOrEmpty(serilogOptions.SeqUrl))
                loggerConfiguration.WriteTo.Seq(serilogOptions.SeqUrl);
        });

        return builder;
    }

    private sealed class SerilogOptions
    {
        public bool UseConsole { get; set; } = true;
        public string? SeqUrl { get; set; } = "http://localhost:5341";
        public string? ElasticSearchUrl { get; set; } = "http://localhost:9200";
        public string LogTemplate { get; set; } =
            "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} {Level} - {Message:lj}{NewLine}{Exception}";
    }
}