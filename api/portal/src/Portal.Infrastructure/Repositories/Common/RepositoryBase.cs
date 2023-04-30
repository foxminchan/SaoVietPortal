using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Portal.Domain.Interfaces.Common;
using System.Reflection;
using Portal.Domain.Specification;

namespace Portal.Infrastructure.Repositories.Common;

public abstract class RepositoryBase<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    protected RepositoryBase(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public virtual IEnumerable<T> GetAll() => _dbSet.AsNoTracking();

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

    public virtual int Count() => _dbSet.Count();

    public virtual bool TryGetById(object? id, out T? entity)
    {
        entity = _dbSet.Find(id);
        return entity is not null;
    }

    public virtual IQueryable<T> GetList(Criteria<T> criteria)
    {
        IQueryable<T> query = _dbSet;

        if (criteria.Filter is not null)
        {
            query = query.Where(criteria.Filter);

            var filterBody = criteria.Filter.Body.ToString();

            if (filterBody.Contains("()") || filterBody.Contains("new "))
                throw new ArgumentException("Invalid filter expression.");
        }

        if (criteria.IncludeProperties is not null)
            query = criteria.IncludeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        if (criteria.OrderBy is not null)
            query = criteria.OrderBy(query);

        if (criteria.Skip != 0)
            _ = query.Skip(criteria.Skip);

        if (criteria.Take != 0)
            _ = query.Take(criteria.Take);

        return string.IsNullOrEmpty(criteria.Fields)
            ? query.AsNoTracking()
            : GetField(criteria, query);
    }

    public IQueryable<T> GetField(Criteria<T> criteria, IQueryable<T> query)
    {
        var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var selectedProperties = criteria.Fields.Split(',')
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(x => propertyInfos.FirstOrDefault(p => p.Name.Equals(x, StringComparison.InvariantCultureIgnoreCase)))
            .Where(x => x is not null);

        var parameter = Expression.Parameter(typeof(T), "x");

        var memberBindings = selectedProperties
            .Select(property => Expression
            .Bind(property ?? throw new ArgumentNullException(nameof(property)),
                Expression.Property(parameter, property)));

        var memberInit = Expression.MemberInit(Expression.New(typeof(T)), memberBindings);

        var selector = Expression.Lambda<Func<T, T>>(memberInit, parameter);

        return query.Select(selector).AsNoTracking();
    }

    public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where) => _dbSet.Where(where);

    public virtual bool Any(Expression<Func<T, bool>> where) => _dbSet.Any(where);
}