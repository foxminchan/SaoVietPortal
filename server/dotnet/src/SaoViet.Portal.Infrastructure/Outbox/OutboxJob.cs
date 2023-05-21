using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;
using SaoViet.Portal.Domain.Interfaces;
using SaoViet.Portal.Infrastructure.Persistence;

namespace SaoViet.Portal.Infrastructure.Outbox;

public class OutboxJob : IJob
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IPublisher _publisher;

    public OutboxJob(ApplicationDbContext dbContext, IPublisher publisher)
        => (_dbContext, _publisher) = (dbContext, publisher);

    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await _dbContext
            .Set<OutboxEntity>()
            .Where(e => e.ProcessedDate == null)
            .Take(20)
            .ToListAsync(context.CancellationToken);

        foreach (var message in messages)
        {
            var domainEvent = JsonConvert
                .DeserializeObject<IDomainEvent>(message.Data ?? throw new InvalidOperationException());

            if (domainEvent is null) continue;

            await _publisher.Publish(domainEvent, context.CancellationToken);

            message.ProcessedDate = DateTime.UtcNow;
        }

        await _dbContext.SaveChangesAsync(context.CancellationToken);
    }
}