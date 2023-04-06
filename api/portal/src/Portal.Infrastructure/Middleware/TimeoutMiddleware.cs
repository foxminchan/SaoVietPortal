using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Portal.Infrastructure.Middleware;

public class TimeoutMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TimeoutMiddleware> _logger;

    public TimeoutMiddleware(RequestDelegate next, ILogger<TimeoutMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        using var timeoutCts = new CancellationTokenSource();
        var completedTask = await Task.WhenAny(_next(httpContext), Task.Delay(TimeSpan.FromMinutes(2), timeoutCts.Token));
        if (completedTask.IsCompleted)
            timeoutCts.Cancel();
        else
        {
            _logger.LogError("Request timed out");
            httpContext.Response.StatusCode = StatusCodes.Status408RequestTimeout;
        }
    }
}