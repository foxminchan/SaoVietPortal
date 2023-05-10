using Portal.Shared.Infrastructure.ResultModels.Interfaces.Common;

namespace Portal.Shared.Infrastructure.ResultModels.Interfaces.IWrite;

public interface IDeleteCommand<T> : ICommand
    where T : struct
{
    public T Id { get; init; }
}