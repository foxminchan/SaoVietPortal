using Portal.Domain.Specification;
using System.Linq.Expressions;

namespace Portal.Domain.Interfaces.Common;

public interface IRepository<T> where T : class
{
    public void Insert(T entity);

    public void Update(T entity);

    public void Update(T entity, params Expression<Func<T, object>>[] properties);

    public void Delete(T entity);

    public void Delete(Expression<Func<T, bool>> where);

    public bool TryGetById(object? id, out T? entity);

    public int Count();

    public IQueryable<T> GetList(Criteria<T> criteria);

    public IQueryable<T> GetField(Criteria<T> criteria, IQueryable<T> query);

    public IEnumerable<T> GetAll();

    public IEnumerable<T> GetMany(Expression<Func<T, bool>> where);

    public bool Any(Expression<Func<T, bool>> where);
}