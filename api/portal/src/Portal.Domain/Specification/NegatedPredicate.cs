using System.Linq.Expressions;

namespace Portal.Domain.Specification;

public class NegatedPredicate<T> : SpecificationBase<T>
{
    private readonly ISpecification<T> _inner;

    public NegatedPredicate(ISpecification<T> inner) => _inner = inner;

    public override Expression<Func<T, bool>> Criteria
    {
        get
        {
            var objParam = Expression.Parameter(typeof(T), "obj");

            var newExpr = Expression.Lambda<Func<T, bool>>(
                Expression.Not(
                    Expression.Invoke(_inner.Criteria, objParam)
                ),
                objParam
            );

            return newExpr;
        }
    }
}