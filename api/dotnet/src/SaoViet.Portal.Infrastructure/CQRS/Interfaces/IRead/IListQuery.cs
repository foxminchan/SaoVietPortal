using SaoViet.Portal.Domain.Specification;
using SaoViet.Portal.Infrastructure.CQRS.Interfaces.Common;

namespace SaoViet.Portal.Infrastructure.CQRS.Interfaces.IRead;

public interface IListQuery : IQuery
{
    public List<string> Includes { get; init; }
    public List<FilterModel> Filters { get; init; }
    public List<string> Sorts { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}