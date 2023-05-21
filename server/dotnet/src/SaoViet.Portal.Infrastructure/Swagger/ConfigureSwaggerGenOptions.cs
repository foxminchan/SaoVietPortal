using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SaoViet.Portal.Infrastructure.Swagger;

public class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "Sao Viet Portal APIs",

                    Version = description.ApiVersion.ToString(),

                    Description = """
                     Sao Viet Portal is an open source platform designed to manage and organize student information for the Sao Viet.With this portal, students, teachers, and staff can easily access and update student records, such as attendance, grades, and personal information.If you have any questions, please contact the author via email `nguyenxuannhan407@gmail.com` or guide email `nd.anh@hutech.edu.vn`

                     Some useful links for you:
                     - [**Github repository * *](https://github.com/foxminchan/SaoVietPortal)
                     - [**Company * *](https://blogdaytinhoc.com)
                     - [**Redoc * *](https://localhost:8080/api-docs)
                     """,

                    Contact = new OpenApiContact
                    {
                        Name = "Send email to author",
                        Email = "nguyenxuannhan407@gmail.com"
                    },

                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    },

                    TermsOfService
                        = new Uri(
                            "https://blogdaytinhoc.com/chinh-sach-chung-va-dieu-khoan-trung-tam-tin-hoc-sao-viet-222"),

                    Extensions =
                    {
                    {
                        "x-logo", new OpenApiObject
                            {
                                { "url", new OpenApiString("https://i.imgur.com/Y8oYCOj.jpeg") },
                                { "altText", new OpenApiString("Sao Viet Portal") },
                                { "backgroundColor", new OpenApiString("#FFFFFF") },
                                { "href", new OpenApiString("") }
                            }
                        }
                }
        });

        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = JwtBearerDefaults.AuthenticationScheme
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Name = JwtBearerDefaults.AuthenticationScheme,
                        In = ParameterLocation.Header,
                        Reference = new OpenApiReference
                        {
                            Id = JwtBearerDefaults.AuthenticationScheme,
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                }
            });

        options.ResolveConflictingActions(apiDescription => apiDescription.First());
        options.EnableAnnotations();
    }
}
}