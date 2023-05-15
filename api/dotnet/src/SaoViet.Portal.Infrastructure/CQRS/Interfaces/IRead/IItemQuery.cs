using SaoViet.Portal.Infrastructure.CQRS.Interfaces.Common;

namespace SaoViet.Portal.Infrastructure.CQRS.Interfaces.IRead;

public interface IItemQuery<T> : IQuery
    where T : struct
{
    public T Id { get; init; }
    public List<string> IncludeStrings { get; init; }
}