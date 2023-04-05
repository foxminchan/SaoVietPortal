using Portal.Domain.Specification;
using System.Linq.Expressions;

namespace Portal.Domain.Interfaces.Common;

public interface IGridRepository<T> where T : class
{
    public int Count(IGridSpecification<T> spec);
    public IEnumerable<T> GetList(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string includeProperties = "",
        int skip = 0,
        int take = 0);
    public IEnumerable<T> GetAll();
    public IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
    public bool Any(Expression<Func<T, bool>> where);
}