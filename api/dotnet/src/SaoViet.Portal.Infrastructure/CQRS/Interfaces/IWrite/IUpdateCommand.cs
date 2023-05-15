using SaoViet.Portal.Domain.Interfaces;
using SaoViet.Portal.Infrastructure.CQRS.Interfaces.Common;

namespace SaoViet.Portal.Infrastructure.CQRS.Interfaces.IWrite;

public interface IUpdateCommand<T> : ICommand, ITxRequest
    where T : struct
{
    public T Id { get; init; }
}