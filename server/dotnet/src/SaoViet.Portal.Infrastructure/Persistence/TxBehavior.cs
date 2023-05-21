using System.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SaoViet.Portal.Domain.DomainEvents;
using SaoViet.Portal.Domain.Interfaces;

namespace SaoViet.Portal.Infrastructure.Persistence;

public class TxBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    private readonly IDomainEventContext _domainEventContext;
    private readonly IDatabaseFacade _dbFacadeResolver;
    private readonly IPublisher _publisher;

    public TxBehavior(
        IDatabaseFacade dbFacadeResolver,
        IDomainEventContext domainEventContext,
        IPublisher publisher)
        => (_dbFacadeResolver, _domainEventContext, _publisher)
            = (dbFacadeResolver, domainEventContext, publisher);

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is not ITxRequest)
            return await next();

        var strategy = _dbFacadeResolver.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _dbFacadeResolver.Database
                .BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

            var response = await next();

            var domainEvents = _domainEventContext.GetDomainEvents<object>().ToList();

            var tasks = domainEvents
                .Select(async domainEvent => await _publisher
                    .Publish(new EventWrapper(domainEvent), cancellationToken));

            await Task.WhenAll(tasks).ConfigureAwait(false);

            await transaction.CommitAsync(cancellationToken);

            return response;
        }).ConfigureAwait(false);
    }
}