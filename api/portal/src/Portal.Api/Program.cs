using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Portal.Application.Services;
using Portal.Infrastructure;
using System.IO.Compression;
using FluentValidation;
using HealthChecks.UI.Client;
using Portal.Api.Extensions;
using Portal.Api.Validations;
using Portal.Api.Models;
using Portal.Application.Transaction;
using Portal.Infrastructure.Middleware;
using Swashbuckle.AspNetCore.SwaggerGen;
using Serilog;
using Microsoft.AspNetCore.Diagnostics;
using Portal.Application.Search;
using Portal.Domain.Interfaces.Common;
using Portal.Infrastructure.Errors;
using Portal.Infrastructure.Repositories.Common;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Diagnostics.HealthChecks;

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

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ApiVersionReader = new HeaderApiVersionReader("api-version");
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();
builder.Services.AddResponseCaching();
builder.Services.AddRateLimiting();
builder.Services.AddRedisCache(builder.Configuration);
builder.Services.Configure<SwaggerGeneratorOptions>(o => o.InferSecuritySchemes = true);
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Dev", authorizationPolicyBuilder => authorizationPolicyBuilder
        .RequireClaim("DevClaim", "Developer")
        .RequireAuthenticatedUser());
    options.FallbackPolicy = null;
});

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

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(_ =>
    {
        _.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddTransient<StudentService>();
builder.Services.AddTransient<TransactionService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IValidator<Student>, StudentValidator>();

builder.Services.AddSingleton<IDeveloperPageExceptionFilter, DeveloperPageExceptionFilter>();
builder.Services.AddSingleton<ILuceneService, LuceneService>(_ => new LuceneService("lucene-index"));

builder.AddOpenTelemetry();
builder.AddSerilog();
builder.AddOpenApi();

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

app.UseCors();
app.UseResponseCaching();
app.UseResponseCompression();
app.UseStaticFiles();
app.UseRateLimiter();
app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseHttpsRedirection();
app.MapHealthChecks("/health", new HealthCheckOptions()
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
app.MapControllers();
app.MapPrometheusScrapingEndpoint();
app.Run();
