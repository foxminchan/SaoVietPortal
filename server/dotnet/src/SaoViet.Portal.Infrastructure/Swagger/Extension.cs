using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SaoViet.Portal.Infrastructure.Swagger;

public static class Extension
{
    public static void AddOpenApi(this IServiceCollection services)
        => services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>()
            .AddFluentValidationRulesToSwagger()
            .AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>())
            .AddSwaggerGenNewtonsoftSupport()
            .Configure<SwaggerGeneratorOptions>(o => o.InferSecuritySchemes = true);
}