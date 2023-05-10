using Portal.Shared.Core.Interfaces;
using Portal.Shared.Infrastructure.ResultModels.Interfaces.Common;

namespace Portal.Shared.Infrastructure.ResultModels.Interfaces.IWrite;

public interface IUpdateCommand<T> : ICommand, ITxRequest
    where T : struct
{
    public T Id { get; init; }
}