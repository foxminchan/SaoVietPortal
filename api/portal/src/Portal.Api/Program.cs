using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Portal.Application.Services;
using Portal.Infrastructure;
using System.IO.Compression;
using FluentValidation;
using Portal.Api.Extensions;
using Portal.Api.Validations;
using Portal.Api.Models;
using Portal.Application.Transaction;
using Portal.Infrastructure.Middleware;
using Swashbuckle.AspNetCore.SwaggerGen;
using Serilog;
using Microsoft.AspNetCore.Diagnostics;
using Portal.Infrastructure.ErrorHandler;

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
});

builder.Services.AddResponseCaching();
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

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();
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
    }));
    options.EnableDetailedErrors();
    options.EnableSensitiveDataLogging();
});

builder.Services.AddTransient<StudentService>();
builder.Services.AddTransient<TransactionService>();

builder.Services.AddScoped<IValidator<Student>, StudentValidator>();

builder.Services.AddSingleton<IDeveloperPageExceptionFilter, DeveloperPageExceptionFilter>();

builder.AddOpenTelemetry();
builder.AddSerilog();
builder.AddOpenApi();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<SecurityHeadersMiddleware>();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseDeveloperExceptionPage();
    app.UseHsts();
}

app.UseResponseCaching();
app.UseResponseCompression();
app.UseStaticFiles();
app.UseRateLimiter();
app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseHttpsRedirection();
app.MapControllers();
app.MapPrometheusScrapingEndpoint();
app.Run();
