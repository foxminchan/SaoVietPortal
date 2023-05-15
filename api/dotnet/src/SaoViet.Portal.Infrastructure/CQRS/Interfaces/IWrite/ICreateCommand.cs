using SaoViet.Portal.Domain.Interfaces;
using SaoViet.Portal.Infrastructure.CQRS.Interfaces.Common;

namespace SaoViet.Portal.Infrastructure.CQRS.Interfaces.IWrite;

public interface ICreateCommand : ICommand, ITxRequest
{
}