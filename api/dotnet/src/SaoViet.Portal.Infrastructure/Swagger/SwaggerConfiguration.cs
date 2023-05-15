using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

namespace SaoViet.Portal.Infrastructure.Swagger;

public static class SwaggerConfiguration
{
    public static void AddOpenApi(this IApplicationBuilder app)
    {
        app.UseSwagger(c =>
        {
            c.RouteTemplate = "swagger/{documentName}/swagger.json";
            c.PreSerializeFilters.Add((swagger, httpReq) =>
            {
                ArgumentNullException.ThrowIfNull(httpReq, nameof(httpReq));

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
                        Name = "CourseRegistration",
                        Description = "Management of course registration",
                        ExternalDocs = new OpenApiExternalDocs
                        {
                            Description = "More details",
                            Url = new Uri("/api-docs/index.html#tag/CourseRegistration", UriKind.RelativeOrAbsolute)
                        }
                    },
                    new()
                    {
                        Name = "StudentProgress",
                        Description = "Management of student progress",
                        ExternalDocs = new OpenApiExternalDocs
                        {
                            Description = "More details",
                            Url = new Uri("/api-docs/index.html#tag/StudentProgress", UriKind.RelativeOrAbsolute)
                        }
                    },
                    new()
                    {
                        Name = "ReceiptsExpenses",
                        Description = "Management of receipts and expenses",
                        ExternalDocs = new OpenApiExternalDocs
                        {
                            Description = "More details",
                            Url = new Uri("/api-docs/index.html#tag/ReceiptsExpenses", UriKind.RelativeOrAbsolute)
                        }
                    },
                    new()
                    {
                        Name = "Class",
                        Description = "Management of class",
                        ExternalDocs = new OpenApiExternalDocs
                        {
                            Description = "More details",
                            Url = new Uri("/api-docs/index.html#tag/Class", UriKind.RelativeOrAbsolute)
                        }
                    },
                    new()
                    {
                        Name = "PaymentMethod",
                        Description = "Management of payment method",
                        ExternalDocs = new OpenApiExternalDocs
                        {
                            Description = "More details",
                            Url = new Uri("/api-docs/index.html#tag/PaymentMethod", UriKind.RelativeOrAbsolute)
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
    }
}