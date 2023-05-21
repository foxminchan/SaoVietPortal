using Microsoft.EntityFrameworkCore.Diagnostics;
using SaoViet.Portal.Domain.Interfaces;

namespace SaoViet.Portal.Infrastructure.Outbox;

public sealed class OutboxInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;

        if (dbContext is null) return base.SavingChangesAsync(eventData, result, cancellationToken);

        var domainEvents = dbContext.ChangeTracker
            .Entries()
            .Select(x => x.Entity)
            .OfType<IAggregateRoot<object>>()
            .SelectMany(x => x.DomainEvents)
            .Select(e => new OutboxEntity(e, null, null))
            .ToList();

        dbContext.Set<OutboxEntity>().AddRange(domainEvents);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}