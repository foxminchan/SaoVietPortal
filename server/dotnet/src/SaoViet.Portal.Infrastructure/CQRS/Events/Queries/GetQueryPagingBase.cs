using MediatR;
using SaoViet.Portal.Domain.Specification;
using SaoViet.Portal.Infrastructure.CQRS.Models;

namespace SaoViet.Portal.Infrastructure.CQRS.Events.Queries;

public record GetQueryPagingBase<T>(ISpecification<T> Specification) : IRequest<ListResultModel<T>>
    where T : BaseModel;