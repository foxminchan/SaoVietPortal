using FluentValidation;
using Hangfire;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Portal.Api.Extensions;
using Portal.Api.Filters;
using Portal.Api.Models;
using Portal.Api.Validations;
using Portal.Application.Health;
using Portal.Application.Search;
using Portal.Application.Services;
using Portal.Application.Token;
using Portal.Application.Transaction;
using Portal.Domain.Interfaces.Common;
using Portal.Infrastructure.Auth;
using Portal.Infrastructure.Middleware;
using Portal.Infrastructure.Repositories.Common;
using Portal.Infrastructure;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.AddServerHeader = false;
    options.AllowResponseHeaderCompression = true;
    options.ConfigureEndpointDefaults(o =>
    {
        o.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;
    });
});

builder.Services.AddControllers(options =>
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

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<GzipCompressionProvider>();
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
    {
        "application/json",
        "application/xml",
        "text/plain"
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
            sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
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
builder.Services.AddTransient<ITokenService, TokenService>();

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

builder.Services.TryAddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
builder.Services.TryAddSingleton(options =>
{
    var provider = options.GetRequiredService<ObjectPoolProvider>();
    var policy = new StringBuilderPooledObjectPolicy();
    return provider.Create(policy);
});

builder.AddApiVersioning();
builder.AddOpenApi();
builder.AddOpenTelemetry();
builder.AddSerilog();
builder.AddHealthCheck();
builder.AddHangfire();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<TimeoutMiddleware>();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseCors();
app.UseExceptionHandler();
app.UseHangfireDashboard();
app.UseHsts();
app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseResponseCaching();
app.UseResponseCompression();
app.UseStaticFiles();
app.UseStatusCodePages();
app.MapHealthCheck();
app.MapGet("/error", () => Results.Problem("An error occurred.", statusCode: 500))
    .ExcludeFromDescription();
app.Map("/", () => Results.Redirect("/swagger"));
app.MapControllers().RequirePerUserRateLimit();
app.MapPrometheusScrapingEndpoint();
app.Run();
