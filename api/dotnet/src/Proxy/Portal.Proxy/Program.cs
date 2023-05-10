using System.IdentityModel.Tokens.Jwt;
using System.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.IdentityModel.Tokens;
using Portal.Proxy.Policies;
using Portal.Shared.Infrastructure.OpenTelemetry;
using Spectre.Console;
using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

AnsiConsole.Write(new FigletText("Proxy").Centered().Color(Color.BlueViolet));

builder.Services.AddProblemDetails();

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = false,
        SignatureValidator = delegate (string token, TokenValidationParameters _)
        {
            var jwt = new JwtSecurityToken(token);
            return jwt;
        }
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Developer", policy => policy
        .RequireClaim("Technical", "Developer")
        .RequireAuthenticatedUser());
    options.FallbackPolicy = null;
});

builder.WebHost.ConfigureKestrel(webBuilder =>
{
    webBuilder.Listen(IPAddress.Any, builder.Configuration.GetValue("ApiPort", 8080));
    webBuilder.ConfigureEndpointDefaults(o => o.Protocols = HttpProtocols.Http1AndHttp2AndHttp3);
});

builder.Services.AddRateLimiting();
builder.Services.AddControllers();
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddTransforms(context =>
    {
        if (string.Equals("Developer", context.Route.AuthorizationPolicy))
        {
            context.AddRequestTransform(async transformContext =>
            {
                var authenticateResult = await transformContext.HttpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);

                if (authenticateResult.Principal?.Claims.Count()! <= 0)
                {
                    var response = transformContext.HttpContext.Response;
                    response.StatusCode = 401;
                }
            });
        }
    });

builder.AddOpenTelemetry();

var app = builder.Build();

app.UseRateLimiter();
app.UseExceptionHandler();
app.UseStatusCodePages();
app.MapReverseProxy().RequirePerUserRateLimit();
app.MapPrometheusScrapingEndpoint();
app.Run();