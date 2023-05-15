using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace SaoViet.Portal.Api.Extensions;

public static class OpenApiExtension
{
    public static void UseOpenApi(this IApplicationBuilder app)
    {
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
    }
}