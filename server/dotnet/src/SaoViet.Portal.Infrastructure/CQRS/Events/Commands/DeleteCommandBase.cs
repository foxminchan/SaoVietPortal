using MediatR;
using SaoViet.Portal.Domain.Interfaces;
using SaoViet.Portal.Infrastructure.CQRS.Models;

namespace SaoViet.Portal.Infrastructure.CQRS.Events.Commands;

public record DeleteCommandBase<T>(object Id) : IRequest<T>, ITxRequest
    where T : BaseModel;