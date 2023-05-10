using Portal.Shared.Infrastructure.ResultModels.Interfaces.Common;

namespace Portal.Shared.Infrastructure.ResultModels.Interfaces.IRead;

public interface IItemQuery<T> : IQuery
    where T : struct
{
    public T Id { get; init; }
    public List<string> IncludeStrings { get; init; }
}