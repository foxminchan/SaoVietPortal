using System.IO.Compression;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SaoViet.Portal.Domain.Interfaces;
using SaoViet.Portal.Infrastructure.Filters;
using SaoViet.Portal.Infrastructure.Health.HealthCheck;
using SaoViet.Portal.Infrastructure.Persistence;
using SaoViet.Portal.Infrastructure.Searching.Lucene;
using SaoViet.Portal.Infrastructure.Searching.Lucene.Internals;

namespace SaoViet.Portal.Api;

public static class DependencyInjection
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.RespectBrowserAcceptHeader = true;
            options.ReturnHttpNotAcceptable = true;
            options.Filters.Add<LoggingFilter>();
        }).AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.ContractResolver = new DefaultContractResolver();
        }).ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressMapClientErrors = true;
            options.SuppressModelStateInvalidFilter = true;
        }).AddXmlDataContractSerializerFormatters();

        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<GzipCompressionProvider>();
            options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
            {
                "application/json",
                "application/xml",
                "text/plain",
                "image/png",
                "image/jpeg"
            });
        });

        services.Configure<GzipCompressionProviderOptions>(options
            => options.Level = CompressionLevel.Optimal);

        services.AddResponseCaching(options =>
        {
            options.MaximumBodySize = 1024;
            options.UseCaseSensitivePaths = true;
        });

        services.AddCors(options =>
            options
                .AddDefaultPolicy(policy => policy
                    .WithOrigins("https://localhost:8000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()));

        //services.AddScoped<IValidator<BranchDTO>, BranchValidator>();
        //services.AddScoped<IValidator<ClassDTO>, ClassValidator>();
        //services.AddScoped<IValidator<CourseDTO>, CourseValidator>();
        //services.AddScoped<IValidator<CourseRegistrationDTO>, CourseRegistrationValidator>();
        //services.AddScoped<IValidator<PaymentMethodDto>, PaymentMethodValidator>();
        //services.AddScoped<IValidator<PositionResponse>, PositionValidator>();
        //services.AddScoped<IValidator<ReceiptsExpensesResponse>, ReceiptsExpensesValidator>();
        //services.AddScoped<IValidator<StaffResponse>, StaffValidator>();
        //services.AddScoped<IValidator<StudentResponse>, StudentValidator>();
        //services.AddScoped<IValidator<StudentProgressResponse>, StudentProgressValidator>();

        services.AddTransient<IUnitOfWork, UnitOfWork>();

        services.AddSingleton<HealthService>();
        services.AddSingleton<IDeveloperPageExceptionFilter, DeveloperPageExceptionFilter>();
        services.AddSingleton(typeof(ILuceneService<>), typeof(LuceneService<>));

        services.TryAddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
        services.TryAddSingleton(options =>
            options
                .GetRequiredService<ObjectPoolProvider>()
                .Create(new StringBuilderPooledObjectPolicy()));
    }
}