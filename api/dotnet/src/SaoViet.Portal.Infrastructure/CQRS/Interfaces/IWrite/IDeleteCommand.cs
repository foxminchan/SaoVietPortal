using SaoViet.Portal.Infrastructure.CQRS.Interfaces.Common;

namespace SaoViet.Portal.Infrastructure.CQRS.Interfaces.IWrite;

public interface IDeleteCommand<T> : ICommand
    where T : struct
{
    public T Id { get; init; }
}