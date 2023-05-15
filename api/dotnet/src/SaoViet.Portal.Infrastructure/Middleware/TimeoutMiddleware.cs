using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SaoViet.Portal.Infrastructure.Middleware;

public class TimeoutMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TimeoutMiddleware> _logger;

    public TimeoutMiddleware(RequestDelegate next, ILogger<TimeoutMiddleware> logger)
        => (_next, _logger) = (next, logger);

    public async Task InvokeAsync(HttpContext httpContext)
    {
        using var timeoutCts = new CancellationTokenSource();

        var completedTask =
            await Task.WhenAny(_next(httpContext), Task.Delay(TimeSpan.FromMinutes(2), timeoutCts.Token));

        if (completedTask.IsCompleted)
        {
            _logger.LogInformation("Request {Path} completed", httpContext.Request.Path);
            timeoutCts.Cancel();
        }
        else
        {
            _logger.LogError("Request {Path} timed out", httpContext.Request.Path);
            httpContext.Response.StatusCode = StatusCodes.Status408RequestTimeout;
        }
    }
}