using System.Text.Json;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SaoViet.Portal.Infrastructure.Validator;

public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    private readonly ILogger<RequestValidationBehavior<TRequest, TResponse>> _logger;
    private readonly IServiceProvider _serviceProvider;

    public RequestValidationBehavior(IServiceProvider serviceProvider,
        ILogger<RequestValidationBehavior<TRequest, TResponse>> logger)
        => (_serviceProvider, _logger) = (serviceProvider, logger);

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "[{Prefix}] Handle request={X-RequestData} and response={X-ResponseData}",
            nameof(RequestValidationBehavior<TRequest, TResponse>), typeof(TRequest).Name, typeof(TResponse).Name);

        _logger.LogDebug(
            "Handled {Request} with content {X-RequestData}",
            typeof(TRequest).FullName, JsonSerializer.Serialize(request));

        var validator = _serviceProvider.GetService<IValidator<TRequest>>();
        if (validator is not null)
            await validator.HandleValidation(request);

        var response = await next();

        _logger.LogInformation(
            "Handled {FullName} with content {Response}",
            typeof(TResponse).FullName, JsonSerializer.Serialize(response));

        return response;
    }
}