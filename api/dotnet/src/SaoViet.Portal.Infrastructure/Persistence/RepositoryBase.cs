using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SaoViet.Portal.Domain.Interfaces;
using SaoViet.Portal.Domain.Specification;

namespace SaoViet.Portal.Infrastructure.Persistence;

public abstract class RepositoryBase<T, TId> : IRepository<T>
    where T : class, IAggregateRoot<TId> where TId : notnull
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    protected RepositoryBase(ApplicationDbContext context) => (_context, _dbSet) = (context, context.Set<T>());

    public virtual IEnumerable<T> GetAll() => _dbSet.AsNoTracking();

    public virtual void Insert(T entity) => _dbSet.Add(entity);

    public virtual void Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public virtual void Update(T entity, params Expression<Func<T, object>>[] properties)
    {
        _dbSet.Attach(entity);
        foreach (var property in properties)
            _context.Entry(entity).Property(property).IsModified = true;
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

    public virtual T? GetById(object? id) => _dbSet.Find(id);

    public virtual IQueryable<T> GetList(ISpecification<T> specification)
    {
        var query = _dbSet.AsQueryable();

        if (specification.Filter is not null)
            _ = query.Where(specification.Filter);

        _ = specification
            .Includes
            .Aggregate(query, (current, include) => current.Include(include));

        _ = specification
            .IncludeStrings
            .Aggregate(query, (current, include) => current.Include(include));

        if (specification.OrderBy is not null)
            _ = query.OrderBy(specification.OrderBy);

        if (specification.OrderByDescending is not null)
            _ = query.OrderByDescending(specification.OrderByDescending);

        if (specification.GroupBy is not null)
            _ = query.GroupBy(specification.GroupBy);

        if (!string.IsNullOrEmpty(specification.Cursor))
        {
            var cursor = JsonConvert.DeserializeObject<T>(specification.Cursor);
            var conversionSuccessful = int.TryParse(
                cursor?
                    .GetType()
                    .GetProperties()
                    .FirstOrDefault(x => x.Name.Equals("Id"))?
                    .GetValue(cursor)?
                    .ToString(),
                out var cursorValue);

            if (conversionSuccessful)
                cursorValue = 0;

            _ = specification.IsAscending
                ? query.Where(x => (int)x.GetType().GetProperty("Id")!.GetValue(x)! > cursorValue)
                : query.Where(x => (int)x.GetType().GetProperty("Id")!.GetValue(x)! < cursorValue);
        }

        if (specification.IsPagingEnabled)
            _ = query.Skip(specification.Skip - 1).Take(specification.Take);

        return !string.IsNullOrEmpty(specification.Fields)
            ? GetFields(specification, query)
            : query.AsSplitQuery().AsNoTracking();
    }

    public IQueryable<T> GetFields(ISpecification<T> specification, IQueryable<T> query)
    {
        var propertyInfos = typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var selectedProperties = specification.Fields?
            .Split(',')
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(x => propertyInfos
                .FirstOrDefault(p => p.Name
                    .Equals(x, StringComparison.InvariantCultureIgnoreCase)))
            .Where(x => x is not null);

        var parameter = Expression.Parameter(typeof(T), "x");

        var memberBindings = selectedProperties?
            .Select(property => Expression
            .Bind(property ?? throw new ArgumentNullException(nameof(property)),
                Expression.Property(parameter, property)));

        ArgumentNullException.ThrowIfNull(memberBindings, nameof(memberBindings));

        var memberInit = Expression.MemberInit(Expression.New(typeof(T)), memberBindings);

        var selector = Expression.Lambda<Func<T, T>>(memberInit, parameter);

        return query.Select(selector).AsNoTracking();
    }

    public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where) => _dbSet.Where(where);

    public virtual bool Any(Expression<Func<T, bool>> where) => _dbSet.Any(where);
}