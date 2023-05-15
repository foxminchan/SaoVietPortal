using System.Linq.Expressions;
using SaoViet.Portal.Domain.Specification;

namespace SaoViet.Portal.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    public void Insert(T entity);

    public void Update(T entity);

    public void Update(T entity, params Expression<Func<T, object>>[] properties);

    public void Delete(T entity);

    public void Delete(Expression<Func<T, bool>> where);

    public T? GetById(object? id);

    public int Count();

    public IQueryable<T> GetList(ISpecification<T> criteria);

    public IQueryable<T> GetFields(ISpecification<T> criteria, IQueryable<T> query);

    public IEnumerable<T> GetAll();

    public IEnumerable<T> GetMany(Expression<Func<T, bool>> where);

    public bool Any(Expression<Func<T, bool>> where);
}