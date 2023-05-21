using MediatR;
using SaoViet.Portal.Infrastructure.CQRS.Models;

namespace SaoViet.Portal.Infrastructure.CQRS.Events.Queries;

public record GetQueryByIdBase<T>(object Id) : IRequest<ResultModel<T>>
    where T : BaseModel;