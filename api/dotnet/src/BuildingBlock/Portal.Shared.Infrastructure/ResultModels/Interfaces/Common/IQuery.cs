using MediatR;
using Microsoft.AspNetCore.Http;

namespace Portal.Shared.Infrastructure.ResultModels.Interfaces.Common;

public interface IQuery : IRequest<IResult>
{
}