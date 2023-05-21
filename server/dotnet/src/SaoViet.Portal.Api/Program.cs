using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore.Migrations;
using SaoViet.Portal.Api.Extensions;
using SaoViet.Portal.Infrastructure;
using SaoViet.Portal.Infrastructure.Persistence;
using SaoViet.Portal.Infrastructure.Swagger;
using Spectre.Console;

AnsiConsole.Write(new FigletText("Sao Viet APIs").Centered().Color(Color.BlueViolet));

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.AddServerHeader = false;
    options.AllowResponseHeaderCompression = true;
    options.ConfigureEndpointDefaults(o => o.Protocols = HttpProtocols.Http1AndHttp2AndHttp3);
});

builder.Services.AddRateLimiting();
builder.Services.AddServiceInfrastructure(builder);

builder.AddWebInfrastructure();

var migration = new MigrationBuilder("SaoViet.Portal.Infrastructure.Persistence.Migrations");
migration.MigrateDataFromScript();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.AddOpenApi()
        .UseDeveloperExceptionPage()
        .UseHsts();
else
    app.UseExceptionHandler("/error");

app.UseRateLimiter();
app.UseWebInfrastructure();
app.MapControllers().RequirePerUserRateLimit();

await app.DoDbMigrationAsync(app.Logger);

app.MapPrometheusScrapingEndpoint();

app.Run();