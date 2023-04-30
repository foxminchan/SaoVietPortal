using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Portal.Api.Filters;
using Portal.Api.Models;
using Portal.Api.Validations;
using Portal.Application.Health;
using Portal.Application.Search;
using Portal.Application.Token;
using Portal.Application.Transaction;
using Portal.Domain.Interfaces.Common;
using Portal.Infrastructure.Repositories.Common;
using System.IO.Compression;

namespace Portal.Api;

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

        //services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
        //    .AddEntityFrameworkStores<ApplicationDbContext>()
        //    .AddDefaultTokenProviders();

        services.AddScoped<IValidator<Branch>, BranchValidator>();
        services.AddScoped<IValidator<Class>, ClassValidator>();
        services.AddScoped<IValidator<Course>, CourseValidator>();
        services.AddScoped<IValidator<CourseRegistration>, CourseRegistrationValidator>();
        services.AddScoped<IValidator<PaymentMethod>, PaymentMethodValidator>();
        services.AddScoped<IValidator<Position>, PositionValidator>();
        services.AddScoped<IValidator<ReceiptsExpenses>, ReceiptsExpensesValidator>();
        services.AddScoped<IValidator<Staff>, StaffValidator>();
        services.AddScoped<IValidator<Student>, StudentValidator>();
        services.AddScoped<IValidator<StudentProgress>, StudentProgressValidator>();

        services.AddTransient<ITokenService, TokenService>();
        services.AddTransient<ITokenService, TokenService>();
        services.AddTransient<ITransactionService, TransactionService>();
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