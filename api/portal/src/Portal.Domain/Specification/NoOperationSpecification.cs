using System.Linq.Expressions;

namespace Portal.Domain.Specification;

public class NoOperationSpecification<T> : SpecificationBase<T>
{
    public override Expression<Func<T, bool>> Criteria => p => true;
}