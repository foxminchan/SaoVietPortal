using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SaoViet.Portal.Domain.Interfaces;
using SaoViet.Portal.Infrastructure.Cache.Redis;
using SaoViet.Portal.Infrastructure.CQRS.Events.Queries;
using SaoViet.Portal.Infrastructure.CQRS.Models;

namespace SaoViet.Portal.Infrastructure.CQRS.Handlers.Queries;

public abstract class GetQueryHandlerBase<TRead, TData, TEntity> : IRequestHandler<TRead, ListResultModel<TData>>
    where TRead : GetQueryBase<TData>
    where TData : BaseModel
    where TEntity : class, IAggregateRoot<object>
{
    private readonly ILogger<GetQueryHandlerBase<TRead, TData, TEntity>> _logger;
    private readonly IRepository<TEntity> _repository;
    private readonly IMapper _mapper;
    private readonly IRedisCacheService _redisCacheService;

    protected GetQueryHandlerBase(
        ILogger<GetQueryHandlerBase<TRead, TData, TEntity>> logger,
        IRepository<TEntity> repository,
        IMapper mapper,
        IRedisCacheService redisCacheService)
        => (_logger, _repository, _mapper, _redisCacheService) = (logger, repository, mapper, redisCacheService);

    public Task<ListResultModel<TData>> Handle(TRead request, CancellationToken cancellationToken)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            var key = $"{typeof(TEntity).Name}Data";

            var data = _redisCacheService
                .GetOrSet(key, _repository.GetAll)
                .Select(_mapper.Map<TData>)
                .Distinct()
                .ToList();

            return Task.FromResult(ListResultModel<TData>.Create(data));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error from {Request} query handler of {Name}", request, typeof(TEntity).Name);
            return Task.FromException<ListResultModel<TData>>(e);
        }
    }
}