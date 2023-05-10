using System.Linq.Expressions;
using Portal.Shared.Core.Interfaces;

namespace Portal.Shared.Core.Specifications;

public class Negated<T> : SpecificationBase<T>
{
    private readonly ISpecification<T> _inner;

    public Negated(ISpecification<T> inner) => _inner = inner;

    public override Expression<Func<T, bool>> Filter
    {
        get
        {
            var parameter = Expression.Parameter(typeof(T), "obj");

            return Expression.Lambda<Func<T, bool>>(
                Expression.Not(
                    Expression.Invoke(_inner.Filter ?? throw new InvalidOperationException(), parameter)
                ), parameter);
        }
    }
}