using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace SaoViet.Portal.Infrastructure.Filters;

public class LoggingFilter : IActionFilter
{
    private readonly ILogger<LoggingFilter> _logger;

    public LoggingFilter(ILogger<LoggingFilter> logger) => _logger = logger;

    public void OnActionExecuting(ActionExecutingContext context)
        => _logger.LogInformation("Action executing: {DisplayName}", context.ActionDescriptor.DisplayName);

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is not null)
            _logger.LogError(context.Exception, "Action failed: {.DisplayName}", context.ActionDescriptor.DisplayName);
        else
            _logger.LogInformation("Action executed: {DisplayName}", context.ActionDescriptor.DisplayName);
    }
}