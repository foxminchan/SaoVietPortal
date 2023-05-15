using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SaoViet.Portal.Infrastructure.CQRS;
using SaoViet.Portal.Infrastructure.Validator;

namespace SaoViet.Portal.Infrastructure.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        => (_next, _logger) = (next, logger);

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong");
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        if (exception is ValidationException { ValidationResultModel.Errors: not null } validationException)
        {
            var validationErrorModel = ResultModel<string>.Create(validationException.ValidationResultModel
                    .Errors.Aggregate("", (a, b) => a + $"{b.Field}-{b.Message}\n"), true, "Validation Error.")
                .ToString();

            await context.Response.WriteAsync(validationErrorModel);
        }
        else
            await context.Response.WriteAsync(
                ResultModel<string>.Create("", true, "Internal Server Error.").ToString());
    }
}