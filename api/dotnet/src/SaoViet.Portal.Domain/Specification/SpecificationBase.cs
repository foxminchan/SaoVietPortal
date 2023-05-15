using System.Linq.Expressions;

namespace SaoViet.Portal.Domain.Specification;

public abstract class SpecificationBase<T> : ISpecification<T>
{
    public abstract Expression<Func<T, bool>> Filter { get; }
    public List<Expression<Func<T, object>>> Includes { get; } = new();
    public List<string> IncludeStrings { get; } = new();
    public Expression<Func<T, object>> OrderBy { get; private set; } = null!;
    public Expression<Func<T, object>> OrderByDescending { get; private set; } = null!;
    public Expression<Func<T, object>> GroupBy { get; private set; } = null!;
    public string? Cursor { get; private set; }
    public string? Fields { get; private set; }
    public int Take { get; private set; }
    public int Skip { get; private set; }
    public bool IsAscending { get; private set; }
    public bool IsPagingEnabled { get; private set; }

    protected void AddIncludeList(IEnumerable<Expression<Func<T, object>>> includes)
        => Includes.AddRange(includes);

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
        => Includes.Add(includeExpression);

    protected void ApplyIncludeList(IEnumerable<string> includes)
        => IncludeStrings.AddRange(includes);

    protected void AddInclude(string includeString)
        => IncludeStrings.Add(includeString);

    protected void ApplyPaging(int skip, int take)
        => (Skip, Take, IsPagingEnabled) = (skip, take, true);

    protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        => OrderBy = orderByExpression;

    protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        => OrderByDescending = orderByDescendingExpression;

    protected void ApplyGroupBy(Expression<Func<T, object>> groupByExpression)
        => GroupBy = groupByExpression;

    protected void ApplySorting(string sort)
        => this.ApplySorting(sort, nameof(ApplyOrderBy), nameof(ApplyOrderByDescending));

    private Func<T, bool>? _compiledExpression;

    private Func<T, bool> CompiledExpression
    {
        get { return _compiledExpression ??= Filter.Compile(); }
    }

    public bool IsSatisfiedBy(T entity)
        => CompiledExpression(entity);

    public void ApplyCursor(string cursor)
        => Cursor = cursor;

    public void ApplyFields(string fields)
        => Fields = fields;

    public void ApplyAscending(bool isAscending)
        => IsAscending = isAscending;
}