using System.Linq.Expressions;

namespace SaoViet.Portal.Domain.Specification;

public interface ISpecification<T> : IRootSpecification
{
    public Expression<Func<T, bool>>? Filter { get; }
    public List<Expression<Func<T, object>>> Includes { get; }
    public List<string> IncludeStrings { get; }
    public Expression<Func<T, object>>? OrderBy { get; }
    public Expression<Func<T, object>>? OrderByDescending { get; }
    public Expression<Func<T, object>>? GroupBy { get; }
    public string? Cursor { get; }
    public string? Fields { get; }
    public int Take { get; }
    public int Skip { get; }
    public bool IsAscending { get; }
    public bool IsPagingEnabled { get; }
}