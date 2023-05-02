using Hangfire;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Portal.Api;
using Portal.Api.Extensions;
using Portal.Infrastructure;
using Portal.Infrastructure.Auth;
using Portal.Infrastructure.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.AddServerHeader = false;
    options.AllowResponseHeaderCompression = true;
    options.ConfigureEndpointDefaults(o => o.Protocols = HttpProtocols.Http1AndHttp2AndHttp3);
});

builder.Services.AddAuth();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddPollyPolicy();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFluentValidationRulesToSwagger();
builder.Services.AddProblemDetails();
builder.Services.AddRateLimiting();
builder.Services.AddRedisCache(builder.Configuration);

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

builder.Services.AddDependencyInjection();

builder.AddApiVersioning();
builder.AddOpenApi();
builder.AddOpenTelemetry();
builder.AddSerilog();
builder.AddHealthCheck();

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
    app.UseExceptionHandler("/error");

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