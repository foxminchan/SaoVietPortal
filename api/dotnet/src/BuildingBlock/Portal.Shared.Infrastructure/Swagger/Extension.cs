using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Portal.Shared.Infrastructure.Swagger;

public static class Extension
{
    public static void AddOpenApi(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();
        services.AddSwaggerGenNewtonsoftSupport();
        services.Configure<SwaggerGeneratorOptions>(o => o.InferSecuritySchemes = true);
        services.AddSwaggerGen();
    }

    public static void UseOpenApi(this IApplicationBuilder app)
    {
        app.UseSwagger(options =>
        {
            options.RouteTemplate = "swagger/{documentName}/swagger.json";
            options.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
            {
                ArgumentNullException.ThrowIfNull(httpReq, nameof(httpReq));

                swaggerDoc.ExternalDocs = new OpenApiExternalDocs
                {
                    Description = "About Sao Viet",
                    Url = new Uri("https://blogdaytinhoc.com/"),
                };

                swaggerDoc.Servers = new List<OpenApiServer>
                {
                    new()
                    {
                        Url = $"{httpReq.Scheme}://{httpReq.Host.Value}"
                    }
                };
            });
        });

        app.UseSwaggerUI(options =>
        {
            options.DocumentTitle = "Sao Viet API";
            options.DisplayRequestDuration();
            options.EnableValidator();
        });

        app.UseReDoc(options =>
        {
            options.DocumentTitle = "Sao Viet API";
            options.EnableUntrustedSpec();
        });
    }
}