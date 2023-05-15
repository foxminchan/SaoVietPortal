using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SaoViet.Portal.Infrastructure.Logging;
using SaoViet.Portal.Infrastructure.Validator;

namespace SaoViet.Portal.Infrastructure.CQRS;

public static class Extension
{
    public static IServiceCollection AddMediator(
        this IServiceCollection services,
        Action<IServiceCollection>? doMoreActions = null)
    {
        services.AddHttpContextAccessor()
            .AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            });

        doMoreActions?.Invoke(services);

        return services;
    }
}