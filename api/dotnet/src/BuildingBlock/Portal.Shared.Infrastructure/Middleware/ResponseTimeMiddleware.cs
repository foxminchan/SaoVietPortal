using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Portal.Shared.Infrastructure.Middleware;

public class ResponseTimeMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ResponseTimeMiddleware> _logger;

    public ResponseTimeMiddleware(RequestDelegate next, ILogger<ResponseTimeMiddleware> logger)
        => (_next, _logger) = (next, logger);

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        await _next(context);
        stopwatch.Stop();
        var responseTime = stopwatch.Elapsed;

        if (context.Response.StatusCode == StatusCodes.Status200OK)
        {
            if (responseTime.Seconds < 3)
                _logger.LogInformation("Request {method} {path} => {responseTime} s",
                    context.Request.Method, context.Request.Path, responseTime);

            _logger.LogWarning("Request {method} {path} => {responseTime} s",
                context.Request.Method, context.Request.Path, responseTime);
        }
        else
        {
            _logger.LogError("Request {method} {path} => {responseTime} s",
                context.Request.Method, context.Request.Path, responseTime);
        }
    }
}