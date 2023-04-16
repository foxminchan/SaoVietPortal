using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Portal.Api.Filters;

public class SocialSchemeFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema.Properties.TryGetValue("socialNetwork", out var property))
        {
            property.Example = new OpenApiObject
            {
                ["facebook"] = new OpenApiString("https://www.facebook.com/FoxMinChan/"),
                ["zalo"] = new OpenApiString("https://zalo.me/foxminchan")
            };
        }
    }
}