using MediatR;
using Microsoft.AspNetCore.Http;

namespace SaoViet.Portal.Infrastructure.CQRS.Interfaces.Common;

public interface ICommand : IRequest<IResult>
{
}