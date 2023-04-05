using System.Linq.Expressions;

namespace Portal.Domain.Interfaces.Common;

public interface IRepository<T> where T : class
{
    public void Insert(T entity);
    public void Update(T entity);
    public void Delete(T entity);
    public void Delete(Expression<Func<T, bool>> where);
    public T? GetById(object? id);
}