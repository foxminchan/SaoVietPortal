using MediatR;
using SaoViet.Portal.Infrastructure.CQRS.Models;

namespace SaoViet.Portal.Infrastructure.CQRS.Events.Queries;

public record GetQueryBase<T> : IRequest<ListResultModel<T>>
    where T : BaseModel;