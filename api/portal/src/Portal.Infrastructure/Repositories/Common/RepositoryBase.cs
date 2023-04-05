using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Portal.Domain.Interfaces.Common;
using Portal.Domain.Specification;

namespace Portal.Infrastructure.Repositories.Common;

public abstract class RepositoryBase<T> : IRepository<T>, IGridRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    protected RepositoryBase(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public virtual IEnumerable<T> GetAll() => _dbSet;

    public virtual void Insert(T entity) => _dbSet.Add(entity);

    public virtual void Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public virtual void Delete(T entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
            _dbSet.Attach(entity);
        _dbSet.Remove(entity);
    }

    public virtual void Delete(Expression<Func<T, bool>> where)
    {
        var objects = _dbSet.Where(where).AsEnumerable();
        foreach (var obj in objects)
            _dbSet.Remove(obj);
    }

    public virtual int Count(IGridSpecification<T> spec)
    {
        spec.IsPagingEnabled = false;
        return GetQuery(_dbSet, spec).LongCount();
    }

    public virtual T? GetById(object? id) => _dbSet.Find(id);

    public virtual IEnumerable<T> GetList(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string includeProperties = "",
        int skip = 0,
        int take = 0)
    {
        IQueryable<T> query = _dbSet;
        if (filter != null)
            query = query.Where(filter);

        query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        if (orderBy != null)
            query = orderBy(query);

        if (skip != 0)
            _ = query.Skip(skip);

        if (take != 0)
            _ = query.Take(take);

        return query;
    }

    public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where) => _dbSet.Where(where);

    public virtual bool Any(Expression<Func<T, bool>> where) => _dbSet.Any(where);

    private static IQueryable<T> GetQuery(IQueryable<T> inputQuery,
        ISpecification<T> specification)
    {
        var query = inputQuery;

        if (specification.Criteria is not null)
            query = query.Where(specification.Criteria);

        query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

        query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

        if (specification.OrderBy is not null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending is not null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        if (specification.GroupBy is not null)
        {
            query = query
                .GroupBy(specification.GroupBy)
                .SelectMany(x => x);
        }

        if (specification.IsPagingEnabled)
        {
            query = query
                .Skip(specification.Skip - 1)
                .Take(specification.Take);
        }

        query = query.AsSplitQuery();

        return query;
    }

    private static IQueryable<T> GetQuery(IQueryable<T> inputQuery,
        IGridSpecification<T> specification)
    {
        var query = inputQuery;

        if (specification.Criteria is not null && specification.Criteria.Count > 0)
        {
            var expr = specification.Criteria.First();
            for (var i = 1; i < specification.Criteria.Count; i++)
            {
                expr = expr.And(specification.Criteria[i]);
            }

            query = query.Where(expr);
        }

        query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

        query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

        if (specification.OrderBy is not null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending is not null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        if (specification.GroupBy is not null)
        {
            query = query
                .GroupBy(specification.GroupBy)
                .SelectMany(x => x);
        }

        if (specification.IsPagingEnabled)
        {
            query = query
                .Skip(specification.Skip - 1)
                .Take(specification.Take);
        }

        query = query.AsSplitQuery();

        return query;
    }
}