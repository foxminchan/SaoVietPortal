using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace SaoViet.Portal.Infrastructure.Swagger;

public static class SwaggerConfiguration
{
    public static IApplicationBuilder AddOpenApi(this IApplicationBuilder app)
    {
        app.UseSwagger(c =>
        {
            c.RouteTemplate = "swagger/{documentName}/swagger.json";
            c.PreSerializeFilters.Add((swagger, httpReq) =>
            {
                ArgumentNullException.ThrowIfNull(httpReq, nameof(httpReq));

                swagger.Servers = new List<OpenApiServer>
                {
                    new()
                    {
                        Url = $"{httpReq.Scheme}://{httpReq.Host.Value}"
                    }
                };
            });
        });

        app.UseSwaggerUI(c =>
        {
            c.DocumentTitle = "Sao Viet API";
            c.HeadContent = @"
                <link rel='stylesheet' type='text/css' href='/css/swagger-ui.css' />
                <script>
                    var defaultFavicon = document.querySelector('link[rel=""icon""]');
                    if (defaultFavicon) { defaultFavicon.parentNode.removeChild(defaultFavicon); }
                </script>
                <link rel='icon' type='image/png' href='/img/favicon.png' sizes='16x16' />";
            foreach (var description in app.ApplicationServices
                         .GetRequiredService<IApiVersionDescriptionProvider>()
                         .ApiVersionDescriptions)
                c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            c.DisplayRequestDuration();
            c.EnableValidator();
        });

        app.UseReDoc(options =>
        {
            options.DocumentTitle = "Sao Viet API";
            options.HeadContent = @"
                <link rel='icon' type='image/png' href='/img/favicon.png' sizes='16x16' />
                <link rel='stylesheet' type='text/css' href='/css/redoc-ui.css' />";
            foreach (var description in app.ApplicationServices
                         .GetRequiredService<IApiVersionDescriptionProvider>()
                         .ApiVersionDescriptions)
                options.SpecUrl($"/swagger/{description.GroupName}/swagger.json");
            options.EnableUntrustedSpec();
        });

        return app;
    }
}