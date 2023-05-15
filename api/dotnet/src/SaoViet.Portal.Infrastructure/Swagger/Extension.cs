using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SaoViet.Portal.Infrastructure.Swagger;

public static class Extension
{
    public static void AddOpenApi(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();
        services.AddSwaggerGenNewtonsoftSupport();
        services.Configure<SwaggerGeneratorOptions>(o => o.InferSecuritySchemes = true);
        services.AddSwaggerGen();
    }
}