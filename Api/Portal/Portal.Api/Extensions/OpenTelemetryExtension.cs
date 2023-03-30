using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Portal.Api.Extensions;

public static class OpenTelemetryExtension
{
    public static WebApplicationBuilder AddOpenTelemetry(this WebApplicationBuilder builder)
    {
        var resourceBuilder = ResourceBuilder.CreateDefault().AddService(builder.Environment.ApplicationName);
        var otlpEndpoint = builder.Configuration.GetValue<string>("OTLP_ENDPOINT");

        if (!string.IsNullOrWhiteSpace(otlpEndpoint))
        {
            builder.Logging.AddOpenTelemetry(logging =>
            {
                logging.SetResourceBuilder(resourceBuilder)
                       .AddOtlpExporter();
            });
        }

        builder.Services.AddOpenTelemetry()
            .WithMetrics(metrics =>
            {
                metrics.SetResourceBuilder(resourceBuilder)
                       .AddPrometheusExporter()
                       .AddAspNetCoreInstrumentation()
                       .AddRuntimeInstrumentation()
                       .AddHttpClientInstrumentation()
                       .AddEventCountersInstrumentation(c =>
                       {
                           c.AddEventSources(
                               "Microsoft.AspNetCore.Hosting",
                               "Microsoft-AspNetCore-Server-Kestrel",
                               "System.Net.Http",
                               "System.Net.Sockets",
                               "System.Net.NameResolution",
                               "System.Net.Security");
                       });
            })
            .WithTracing(tracing =>
            {
                tracing.SetResourceBuilder(resourceBuilder)
                       .AddAspNetCoreInstrumentation()
                       .AddHttpClientInstrumentation()
                       .AddEntityFrameworkCoreInstrumentation();
                if (!string.IsNullOrWhiteSpace(otlpEndpoint))
                    tracing.AddOtlpExporter();
            })
            .StartWithHost();

        return builder;
    }
}