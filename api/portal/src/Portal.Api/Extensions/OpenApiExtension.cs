﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Portal.Api.Extensions;

public static class OpenApiExtension
{
    public static void AddOpenApi(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(c =>
        {
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            c.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = "Sao Viet Portal",
                    Version = "v1",
                    Description = "API for managing students of Sao Viet. For any questions, please contact `nguyenxuannhan407@gmail.com` or `nd.anh@hutech.edu.vn`",
                    Contact = new OpenApiContact
                    {
                        Name = "Nguyen Xuan Nhan",
                        Email = "nguyenxuannhan407@gmail.com",
                        Url = new Uri("https://www.facebook.com/FoxMinChan/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT"),
                    },
                    TermsOfService = new Uri("https://sites.google.com/view/trungtamtinhocsaoviet")
                });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = JwtBearerDefaults.AuthenticationScheme,
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
            c.ResolveConflictingActions(apiDescription => apiDescription.First());
        });
    }

    public static IApplicationBuilder UseOpenApi(this IApplicationBuilder app)
    {
        var findOutMore = new OpenApiExternalDocs
        {
            Description = "Find out more about Swagger",
            Url = new Uri("https://swagger.io/"),
        };

        app.UseSwagger(c =>
        {
            c.RouteTemplate = "swagger/{documentName}/swagger.json";
            c.SerializeAsV2 = true;
            c.PreSerializeFilters.Add((swagger, httpReq) =>
            {
                if (httpReq is null)
                    throw new ArgumentNullException(nameof(httpReq));
                swagger.ExternalDocs = new OpenApiExternalDocs
                {
                    Description = "About Sao Viet",
                    Url = new Uri("https://blogdaytinhoc.com/"),
                };
                swagger.Servers = new List<OpenApiServer>
                {
                    new()
                    {
                        Url = $"{httpReq.Scheme}://{httpReq.Host.Value}"
                    }
                };
                swagger.Tags = new List<OpenApiTag>
                {
                    new()
                    {
                        Name = "Student",
                        Description = "Management of students",
                        ExternalDocs = findOutMore
                    }
                };
            });
        });

        app.UseSwaggerUI(c =>
        {
            c.DocumentTitle = "Sao Viet API";
            c.InjectStylesheet("/css/swagger-ui.css");
            c.InjectJavascript("/js/swagger-ui.js");
            foreach (var description in app.ApplicationServices
                         .GetRequiredService<IApiVersionDescriptionProvider>()
                         .ApiVersionDescriptions)
            {
                c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });

        return app;
    }
}