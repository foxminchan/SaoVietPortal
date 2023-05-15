using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using SaoViet.Portal.Api;
using SaoViet.Portal.Api.Extensions;
using SaoViet.Portal.Infrastructure.Auth;
using SaoViet.Portal.Infrastructure.Cache;
using SaoViet.Portal.Infrastructure.Health;
using SaoViet.Portal.Infrastructure.Middleware;
using SaoViet.Portal.Infrastructure.OpenTelemetry;
using SaoViet.Portal.Infrastructure.Persistence;
using SaoViet.Portal.Infrastructure.Polly;
using SaoViet.Portal.Infrastructure.Versioning;

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

builder.Services.AddSqlServiceCollection(
    builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException());

builder.Services.AddDependencyInjection();

builder.AddApiVersioning();
builder.AddOpenTelemetry();
builder.AddHealthCheck();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<TimeoutMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseDeveloperExceptionPage();
    app.UseHsts();
}
else
    app.UseExceptionHandler("/error");

app.UseCors();
app.UseExceptionHandler();
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