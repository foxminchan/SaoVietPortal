using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Portal.Domain.Interfaces.Common;
using System.Reflection;

namespace Portal.Infrastructure.Repositories.Common;

public abstract class RepositoryBase<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext Context;
    private readonly DbSet<T> _dbSet;

    protected RepositoryBase(ApplicationDbContext context)
    {
        Context = context;
        _dbSet = Context.Set<T>();
    }

    public virtual IEnumerable<T> GetAll() => _dbSet;

    public virtual void Insert(T entity) => _dbSet.Add(entity);

    public virtual void Update(T entity)
    {
        _dbSet.Attach(entity);
        Context.Entry(entity).State = EntityState.Modified;
    }

    public virtual void Delete(T entity)
    {
        if (Context.Entry(entity).State == EntityState.Detached)
            _dbSet.Attach(entity);
        _dbSet.Remove(entity);
    }

    public virtual void Delete(Expression<Func<T, bool>> where)
    {
        var objects = _dbSet.Where(where).AsEnumerable();
        foreach (var obj in objects)
            _dbSet.Remove(obj);
    }

    public virtual int Count() => _dbSet.Count();

    public virtual bool TryGetById(object? id, out T? entity)
    {
        entity = _dbSet.Find(id);
        return entity is not null;
    }

    public virtual IEnumerable<T> GetList(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string[]? includeProperties = null,
        int skip = 0,
        int take = 0,
        string fields = "")
    {
        IQueryable<T> query = _dbSet;

        if (filter is not null)
        {
            query = query.Where(filter);

            var filterBody = filter.Body.ToString();

            if (filterBody.Contains("()") || filterBody.Contains("new "))
                throw new ArgumentException("Invalid filter expression.");
        }

        if (includeProperties is not null)
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        if (orderBy is not null)
            query = orderBy(query);

        if (skip != 0)
            _ = query.Skip(skip);

        if (take != 0)
            _ = query.Take(take);

        if (string.IsNullOrEmpty(fields))
            return query.AsNoTracking();

        var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var selectedProperties = fields.Split(',').Select(x => x.Trim())
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(x => propertyInfos.FirstOrDefault(p => p.Name.Equals(x, StringComparison.InvariantCultureIgnoreCase)))
            .Where(x => x is not null);

        var parameter = Expression.Parameter(typeof(T), "x");

        var memberBindings = selectedProperties.Select(p => Expression
            .Bind(p ?? throw new ArgumentNullException(nameof(p)), Expression.Property(parameter, p)));

        var memberInit = Expression.MemberInit(Expression.New(typeof(T)), memberBindings);

        var selector = Expression.Lambda<Func<T, T>>(memberInit, parameter);

        _ = query.Select(selector);

        return query.AsNoTracking();
    }

    public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where) => _dbSet.Where(where);

    public virtual bool Any(Expression<Func<T, bool>> where) => _dbSet.Any(where);
}