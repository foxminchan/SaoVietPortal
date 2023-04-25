using System.Linq.Expressions;

namespace Portal.Domain.Specification;

public class Criteria<T> where T : class
{
    public Expression<Func<T, bool>>? Filter = null;
    public Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy = null;
    public string[]? IncludeProperties = null;
    public int Skip = 0;
    public int Take = 0;
    public string Fields = string.Empty;
}