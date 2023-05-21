using AutoMapper;
using Microsoft.Extensions.Logging;
using SaoViet.Portal.Application.Branch.DTOs;
using SaoViet.Portal.Application.Branch.Events;
using SaoViet.Portal.Domain.Interfaces;
using SaoViet.Portal.Infrastructure.Cache.Redis;
using SaoViet.Portal.Infrastructure.CQRS.Handlers.Queries;

namespace SaoViet.Portal.Application.Branch.Queries;

public class GetBranchesQueryHandler : GetQueryHandlerBase<GetBranchesQuery, BranchDto, Domain.Entities.Branch>
{
    public GetBranchesQueryHandler(
        ILogger<GetQueryHandlerBase<GetBranchesQuery, BranchDto, Domain.Entities.Branch>> logger,
        IRepository<Domain.Entities.Branch> repository,
        IMapper mapper,
        IRedisCacheService redisCacheService) : base(logger, repository, mapper, redisCacheService)
    {
    }
}