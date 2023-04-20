using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Portal.Api.Filters;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

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
                    Description =
                        "API for managing students of Sao Viet. For any questions, please contact `nguyenxuannhan407@gmail.com` or `nd.anh@hutech.edu.vn`. \n\nSome useful links:\n- [Sao Viet Portal repository](https://github.com/foxminchan/SaoVietPortal)\n- [Author Facebook profile](https://www.facebook.com/foxminchan)\n- [Author Github profile](https://github.com/foxminchan)",
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
                    TermsOfService = new Uri("https://sites.google.com/view/trungtamtinhocsaoviet"),
                    Extensions = new Dictionary<string, IOpenApiExtension>
                    {
                        {
                            "x-logo",
                            new OpenApiObject
                            {
                                ["url"] = new OpenApiString("https://i.imgur.com/Y8oYCOj.jpeg"),
                                ["backgroundColor"] = new OpenApiString("#FFFFFF"),
                                ["altText"] = new OpenApiString("Sao Viet Logo")
                            }
                        }
                    }
                });

            c.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
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

            c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
            c.OperationFilter<SecurityRequirementsOperationFilter>();
            c.SchemaFilter<SocialSchemeFilter>();
            c.ResolveConflictingActions(apiDescription => apiDescription.First());
        });

        builder.Services.AddSwaggerGenNewtonsoftSupport();
    }

    public static IApplicationBuilder UseOpenApi(this IApplicationBuilder app)
    {
        app.UseSwagger(c =>
        {
            c.RouteTemplate = "swagger/{documentName}/swagger.json";
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
                        ExternalDocs = new OpenApiExternalDocs
                        {
                            Description = "More details",
                            Url = new Uri("/api-docs/index.html#tag/Student", UriKind.RelativeOrAbsolute)
                        }
                    },
                    new()
                    {
                        Name = "Staff",
                        Description = "Management of staffs",
                        ExternalDocs = new OpenApiExternalDocs
                        {
                            Description = "More details",
                            Url = new Uri("/api-docs/index.html#tag/Staff", UriKind.RelativeOrAbsolute)
                        }
                    },
                    new()
                    {
                        Name = "Position",
                        Description = "Management of positions",
                        ExternalDocs = new OpenApiExternalDocs
                        {
                            Description = "More details",
                            Url = new Uri("/api-docs/index.html#tag/Position", UriKind.RelativeOrAbsolute)
                        }
                    },
                    new()
                    {
                        Name = "Branch",
                        Description = "Management of branch",
                        ExternalDocs = new OpenApiExternalDocs
                        {
                            Description = "More details",
                            Url = new Uri("/api-docs/index.html#tag/Branch", UriKind.RelativeOrAbsolute)
                        }
                    },
                    new()
                    {
                        Name = "Course",
                        Description = "Management of course",
                        ExternalDocs = new OpenApiExternalDocs
                        {
                            Description = "More details",
                            Url = new Uri("/api-docs/index.html#tag/Course", UriKind.RelativeOrAbsolute)
                        }
                    },
                    new()
                    {
                        Name = "System",
                        Description = "Management of system",
                        ExternalDocs = new OpenApiExternalDocs
                        {
                            Description = "More details",
                            Url = new Uri("/api-docs/index.html#tag/System", UriKind.RelativeOrAbsolute)
                        }
                    }
                };
            });
        });

        app.UseSwaggerUI(c =>
        {
            c.DocumentTitle = "Sao Viet API";
            c.HeadContent = @"
                <link rel='stylesheet' type='text/css' href='/css/swagger-ui.css' />
                <script src='/js/swagger-ui.js' type='text/javascript'></script>
                <script>
                    var defaultFavicon = document.querySelector('link[rel=""icon""]');
                    if (defaultFavicon) {
                        defaultFavicon.parentNode.removeChild(defaultFavicon);
                    }
                </script>
                <link rel='icon' type='image/png' href='/img/favicon.png' sizes='16x16' />";
            foreach (var description in app.ApplicationServices
                         .GetRequiredService<IApiVersionDescriptionProvider>()
                         .ApiVersionDescriptions)
            {
                c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
            c.DisplayRequestDuration();
            c.EnableValidator();
        });

        app.UseReDoc(options =>
        {
            options.DocumentTitle = "Sao Viet API";
            options.HeadContent = @"<link rel='icon' type='image/png' href='/img/favicon.png' sizes='16x16' />
                <link rel='stylesheet' type='text/css' href='/css/redoc-ui.css' />";
            foreach (var description in app.ApplicationServices
                         .GetRequiredService<IApiVersionDescriptionProvider>()
                         .ApiVersionDescriptions)
            {
                options.SpecUrl($"/swagger/{description.GroupName}/swagger.json");
            }
        });

        return app;
    }
}