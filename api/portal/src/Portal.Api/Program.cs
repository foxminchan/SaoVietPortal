using FluentValidation;
using HealthChecks.UI.Client;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Portal.Api.Extensions;
using Portal.Api.Models;
using Portal.Api.Validations;
using Portal.Application.Health;
using Portal.Application.Search;
using Portal.Application.Services;
using Portal.Application.Transaction;
using Portal.Domain.Interfaces.Common;
using Portal.Infrastructure.Auth;
using Portal.Infrastructure.Errors;
using Portal.Infrastructure.Middleware;
using Portal.Infrastructure.Repositories.Common;
using Portal.Infrastructure;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ConfigureEndpointDefaults(o =>
    {
        o.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;
    });
});

builder.Services.AddControllers(options =>
{
    options.RespectBrowserAcceptHeader = true;
    options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
}).ConfigureApiBehaviorOptions(options =>
{
    options.SuppressMapClientErrors = true;
    options.SuppressModelStateInvalidFilter = true;
}).AddXmlDataContractSerializerFormatters();

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<GzipCompressionProvider>();
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
    {
        "application/octet-stream",
        "application/x-msdownload",
        "application/x-msdos-program",
        "application/x-msmetafile",
        "application/x-ms-shortcut",
    });
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.SmallestSize;
});

builder.Services.AddAuth();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFluentValidationRulesToSwagger();
builder.Services.AddPollyPolicy();
builder.Services.AddProblemDetails();
builder.Services.AddRateLimiting();
builder.Services.AddRedisCache(builder.Configuration);
builder.Services.AddResponseCaching();
builder.Services.Configure<SwaggerGeneratorOptions>(o => o.InferSecuritySchemes = true);

builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 10,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        });
    options.UseLoggerFactory(LoggerFactory.Create(log =>
    {
        log.AddConsole();
        log.AddSerilog(dispose: true);
    }));
    options.EnableDetailedErrors();
    options.EnableSensitiveDataLogging();
});

builder.Services.AddHealthChecks()
    .AddCheck<HealthCheck>(nameof(HealthCheck), tags: new[] { "api" })
    .AddDbContextCheck<ApplicationDbContext>(tags: new[] { "db context" })
    .AddRedis("localhost:6379", tags: new[] { "redis" })
    .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
                  ?? throw new InvalidOperationException(), tags: new[] { "database" });

builder.Services
    .AddHealthChecksUI(options =>
    {
        options.AddHealthCheckEndpoint("Health Check API", "/hc");
        options.SetEvaluationTimeInSeconds(10);
    })
    .AddInMemoryStorage();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(_ =>
    {
        _.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddTransient<BranchService>();
builder.Services.AddTransient<ClassService>();
builder.Services.AddTransient<CourseRegistrationService>();
builder.Services.AddTransient<CourseService>();
builder.Services.AddTransient<PaymentMethodService>();
builder.Services.AddTransient<PositionService>();
builder.Services.AddTransient<ReceiptsExpensesService>();
builder.Services.AddTransient<StaffService>();
builder.Services.AddTransient<StudentProgressService>();
builder.Services.AddTransient<StudentService>();
builder.Services.AddTransient<TransactionService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IValidator<Branch>, BranchValidator>();
builder.Services.AddScoped<IValidator<Class>, ClassValidator>();
builder.Services.AddScoped<IValidator<Course>, CourseValidator>();
builder.Services.AddScoped<IValidator<CourseRegistration>, CourseRegistrationValidator>();
builder.Services.AddScoped<IValidator<PaymentMethod>, PaymentMethodValidator>();
builder.Services.AddScoped<IValidator<Position>, PositionValidator>();
builder.Services.AddScoped<IValidator<ReceiptsExpenses>, ReceiptsExpensesValidator>();
builder.Services.AddScoped<IValidator<Staff>, StaffValidator>();
builder.Services.AddScoped<IValidator<Student>, StudentValidator>();
builder.Services.AddScoped<IValidator<StudentProgress>, StudentProgressValidator>();

builder.Services.AddSingleton<HealthService>();
builder.Services.AddSingleton<IDeveloperPageExceptionFilter, DeveloperPageExceptionFilter>();
builder.Services.AddSingleton<ILuceneService, LuceneService>(_ => new LuceneService("lucene-index"));

builder.AddApiVersioning();
builder.AddOpenApi();
builder.AddOpenTelemetry();
builder.AddSerilog();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<TimeoutMiddleware>();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseDeveloperExceptionPage();
    app.UseHsts();
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseCors();
app.UseResponseCaching();
app.UseResponseCompression();
app.UseStaticFiles();
app.UseRateLimiter();
app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseHttpsRedirection();
app.MapHealthChecks("/hc", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    AllowCachingResponses = false,
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    }
});
app.MapHealthChecksUI(options => options.UIPath = "/hc-ui");
app.MapGet("/error", () => Results.Problem("An error occurred.", statusCode: 500))
    .ExcludeFromDescription();
app.Map("/", () => Results.Redirect("/swagger"));
app.MapControllers().RequirePerUserRateLimit();
app.MapPrometheusScrapingEndpoint();
app.Run();
