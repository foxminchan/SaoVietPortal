using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SaoViet.Portal.Domain.Interfaces;
using SaoViet.Portal.Infrastructure.Cache.Redis;
using SaoViet.Portal.Infrastructure.CQRS.Events.Queries;
using SaoViet.Portal.Infrastructure.CQRS.Models;

namespace SaoViet.Portal.Infrastructure.CQRS.Handlers.Queries;

public abstract class GetQueryByIdHandlerBase<TRead, TData, TEntity> : IRequestHandler<TRead, ResultModel<TData>>
    where TRead : GetQueryByIdBase<TData>
    where TData : BaseModel
    where TEntity : class, IAggregateRoot<object>
{
    private readonly ILogger<GetQueryByIdHandlerBase<TRead, TData, TEntity>> _logger;
    private readonly IRepository<TEntity> _repository;
    private readonly IMapper _mapper;
    private readonly IRedisCacheService _redisCacheService;

    protected GetQueryByIdHandlerBase(
        ILogger<GetQueryByIdHandlerBase<TRead, TData, TEntity>> logger,
        IRepository<TEntity> repository,
        IMapper mapper,
        IRedisCacheService redisCacheService)
        => (_logger, _repository, _mapper, _redisCacheService)
            = (logger, repository, mapper, redisCacheService);

    public virtual Task<ResultModel<TData>> Handle(TRead request, CancellationToken cancellationToken)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            var key = $"{typeof(TEntity).Name}Data";

            if (!_redisCacheService.GetKeys(key).Any())
                return Task.FromResult(ResultModel<TData>
                    .Create(_mapper.Map<TData>(_repository.GetById(request.Id))));

            var data = _redisCacheService
                .GetOrSet(key, _repository.GetAll)
                .FirstOrDefault(x => x.Id.Equals(request.Id));

            return Task.FromResult(ResultModel<TData>.Create(_mapper.Map<TData>(data)));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error from {Request} query handler of {Name}", request, typeof(TEntity).Name);
            return Task.FromResult(ResultModel<TData>.Create(null, true, e.Message));
        }
    }
}