using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SaoViet.Portal.Domain.Interfaces;
using SaoViet.Portal.Infrastructure.Cache.Redis;
using SaoViet.Portal.Infrastructure.CQRS.Events.Queries;
using SaoViet.Portal.Infrastructure.CQRS.Models;
using SaoViet.Portal.Infrastructure.Searching.Lucene;

namespace SaoViet.Portal.Infrastructure.CQRS.Handlers.Queries;

public abstract class GetQueryByFieldHandlerBase<TRead, TData, TEntity> : IRequestHandler<TRead, ListResultModel<TData>>
    where TRead : GetQueryByFieldBase<TData>
    where TData : BaseModel
    where TEntity : class, IAggregateRoot<object>
{
    private readonly ILogger<GetQueryByFieldHandlerBase<TRead, TData, TEntity>> _logger;
    private readonly IRepository<TEntity> _repository;
    private readonly IMapper _mapper;
    private readonly IRedisCacheService _redisCacheService;
    private readonly ILuceneService<TData> _luceneService;

    protected GetQueryByFieldHandlerBase(
               ILogger<GetQueryByFieldHandlerBase<TRead, TData, TEntity>> logger,
               IRepository<TEntity> repository,
               IMapper mapper,
               IRedisCacheService redisCacheService,
               ILuceneService<TData> luceneService)
        => (_logger, _repository, _mapper, _redisCacheService, _luceneService)
            = (logger, repository, mapper, redisCacheService, luceneService);

    public virtual Task<ListResultModel<TData>> Handle(TRead request, CancellationToken cancellationToken)
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

            if (!data.Any())
                return Task.FromResult(ListResultModel<TData>.Create(data));

            if (!_luceneService.IsExistIndex(data.First()))
                _luceneService.Index(data, LuceneOptions.Create.Value);

            var result = _luceneService
                .Search(request.FieldValue, 20)
                .Select(_mapper.Map<TData>)
                .Distinct()
                .ToList();

            return Task.FromResult(ListResultModel<TData>.Create(result));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error from {Request} query handler of {Name}", request, typeof(TEntity).Name);
            return Task.FromException<ListResultModel<TData>>(e);
        }
    }
}