using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Portal.Shared.Infrastructure.Swagger;

public class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        options.SwaggerDoc("APIs v1",
            new OpenApiInfo
            {
                Title = "Portal API",
                Version = "v1",
                Description = """
                Sao Viet Portal is an open source platform designed to manage and organize student information for the Sao Viet.With this portal, students, teachers, and staff can easily access and update student records, such as attendance, grades, and personal information.

                Some useful links:
                - [Sao Viet Portal repository](https://github.com/foxminchan/SaoVietPortal)
                - [Author Facebook profile](https://www.facebook.com/foxminchan)
                - [Author Github profile](https://github.com/foxminchan)
                """,

                Contact = new OpenApiContact
                {
                    Name = "Nguyen Xuan Nhan",
                    Email = "nguyenxuannhan407@gmail.com"
                },

                License = new OpenApiLicense
                {
                    Name = "MIT",
                    Url = new Uri("https://opensource.org/licenses/MIT"),
                },

                TermsOfService
                    = new Uri("https://blogdaytinhoc.com/chinh-sach-chung-va-dieu-khoan-trung-tam-tin-hoc-sao-viet-222"),

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

        options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
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
    }
}