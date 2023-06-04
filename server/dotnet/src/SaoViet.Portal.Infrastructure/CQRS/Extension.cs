using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SaoViet.Portal.Infrastructure.Logging;
using SaoViet.Portal.Infrastructure.Validator;
using System.Diagnostics;
using SaoViet.Portal.Infrastructure.Persistence;

namespace SaoViet.Portal.Infrastructure.CQRS;

public static class Extension
{
    [DebuggerStepThrough]
    public static IServiceCollection AddMediator(
        this IServiceCollection services,
        Action<IServiceCollection>? action = null)
    {
        services.AddHttpContextAccessor()
            .AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>),
                    ServiceLifetime.Scoped);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>),
                    ServiceLifetime.Scoped);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(TxBehavior<,>),
                    ServiceLifetime.Scoped);
            });

        action?.Invoke(services);

        return services;
    }
}