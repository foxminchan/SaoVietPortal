using Portal.Shared.Core.Specifications;
using Portal.Shared.Infrastructure.ResultModels.Interfaces.Common;

namespace Portal.Shared.Infrastructure.ResultModels.Interfaces.IRead;

public interface IListQuery : IQuery
{
    public List<string> Includes { get; init; }
    public List<FilterModel> Filters { get; init; }
    public List<string> Sorts { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}