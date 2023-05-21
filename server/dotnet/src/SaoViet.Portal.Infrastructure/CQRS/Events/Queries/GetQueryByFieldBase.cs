using MediatR;
using SaoViet.Portal.Infrastructure.CQRS.Models;

namespace SaoViet.Portal.Infrastructure.CQRS.Events.Queries;

public record GetQueryByFieldBase<T>(string FieldValue) : IRequest<ListResultModel<T>>
    where T : BaseModel;