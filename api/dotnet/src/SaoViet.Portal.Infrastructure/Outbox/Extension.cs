using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SaoViet.Portal.Domain.DomainEvents;

namespace SaoViet.Portal.Infrastructure.Outbox;

public static class Extension
{
    public static void AddTransactionalOutbox(this IServiceCollection services)
    {
        services.AddSingleton<IEventStorage, EventStorage>();
        services.AddScoped<INotificationHandler<EventWrapper>, LocalDispatchedHandler>();
        services.AddScoped<ITxOutboxProcessor, TxOutboxProcessor>();
    }
}