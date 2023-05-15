using System.Net.Mime;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;

namespace SaoViet.Portal.Infrastructure.Filters;

public class DeveloperPageExceptionFilter : IDeveloperPageExceptionFilter
{
    private static readonly object ErrorContextItemsKey = new();
    private static readonly MediaTypeHeaderValue JsonMediaType = new(MediaTypeNames.Application.Json);

    private static readonly RequestDelegate RespondWithProblemDetails
        = RequestDelegateFactory.Create(
        (HttpContext context) =>
        {
            if (context.Items.TryGetValue(ErrorContextItemsKey, out var errorContextItem) &&
                errorContextItem is ErrorContext errorContext)
                return new ErrorProblemDetailsResult(errorContext.Exception);

            return null;
        }).RequestDelegate;

    public async Task HandleExceptionAsync(ErrorContext errorContext, Func<ErrorContext, Task> next)
    {
        var headers = errorContext.HttpContext.Request.GetTypedHeaders();
        var acceptHeader = headers.Accept;

        if (acceptHeader.Any(h => h.IsSubsetOf(JsonMediaType)))
        {
            errorContext.HttpContext.Items.Add(ErrorContextItemsKey, errorContext);
            await RespondWithProblemDetails(errorContext.HttpContext);
        }
        else
        {
            await next(errorContext);
        }
    }
}

public class ErrorProblemDetailsResult : IResult
{
    private readonly Exception _ex;

    public ErrorProblemDetailsResult(Exception ex) => _ex = ex;

    public async Task ExecuteAsync(HttpContext httpContext)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "An unhandled exception occurred while processing the request",
            Detail = $"{_ex.GetType().Name}: {_ex.Message}",
            Status = _ex switch
            {
                BadHttpRequestException ex => ex.StatusCode,
                _ => StatusCodes.Status500InternalServerError
            }
        };

        problemDetails.Extensions.Add("exception", _ex.GetType().FullName);
        problemDetails.Extensions.Add("stack", _ex.StackTrace);
        problemDetails.Extensions.Add("headers",
            httpContext.Request.Headers
                .ToDictionary(kvp
                    => kvp.Key, kvp => (string)kvp.Value!));
        problemDetails.Extensions.Add("routeValues", httpContext.GetRouteData().Values);
        problemDetails.Extensions.Add("query", httpContext.Request.Query);

        var endpoint = httpContext.GetEndpoint();

        if (endpoint is not null)
        {
            var routeEndpoint = endpoint as RouteEndpoint;
            var httpMethods = endpoint.Metadata.GetMetadata<IHttpMethodMetadata>()?.HttpMethods;
            problemDetails.Extensions.Add("endpoint", new
            {
                endpoint.DisplayName,
                routePattern = routeEndpoint?.RoutePattern.RawText,
                routeOrder = routeEndpoint?.Order,
                httpMethods = httpMethods is not null ? string.Join(", ", httpMethods) : ""
            });
        }

        var result = Results.Json(problemDetails, statusCode: problemDetails.Status,
            contentType: "application/problem+json");

        await result.ExecuteAsync(httpContext);
    }
}