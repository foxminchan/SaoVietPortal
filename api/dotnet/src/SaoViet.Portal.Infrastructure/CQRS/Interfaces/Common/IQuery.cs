using MediatR;
using Microsoft.AspNetCore.Http;

namespace SaoViet.Portal.Infrastructure.CQRS.Interfaces.Common;

public interface IQuery : IRequest<IResult>
{
}