using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Portal.Shared.Infrastructure.Validator;

namespace Portal.Shared.Infrastructure.MediaR;

public static class Extension
{
    public static IServiceCollection AddMediator(
        this IServiceCollection services,
        Type[]? config = default,
        Action<IServiceCollection>? action = null)
    {
        services.AddHttpContextAccessor();

        if (config is not null)
            services.AddMediatR(config)
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

        action?.Invoke(services);

        return services;
    }
}