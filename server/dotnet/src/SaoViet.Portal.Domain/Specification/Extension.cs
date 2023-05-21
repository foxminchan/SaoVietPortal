using System.Linq.Expressions;
using System.Reflection;

namespace SaoViet.Portal.Domain.Specification;

public static class Extension
{
    public static void ApplySorting(this IRootSpecification specification,
        string sort,
        string orderByMethodName,
        string orderByDescendingMethodName)
    {
        if (string.IsNullOrEmpty(sort))
            return;

        const string DescendingSuffix = "Desc";

        var descending = sort.EndsWith(DescendingSuffix, StringComparison.Ordinal);
        var propertyName = string.Concat(sort[..1]
                .ToUpperInvariant(), sort
                .AsSpan(1, sort.Length - 1 - (descending ? DescendingSuffix.Length : 0)));

        var specificationType = specification.GetType().BaseType;
        var targetType = specificationType?.GenericTypeArguments[0];
        var property = targetType!.GetRuntimeProperty(propertyName) ?? throw new InvalidOperationException();

        var lambdaParameterX = Expression.Parameter(targetType ?? throw new InvalidOperationException(), "x");

        var propertyReturningExpression = Expression.Lambda(
            Expression.Convert(
                Expression.Property(lambdaParameterX, property),
                typeof(object)), lambdaParameterX);

        if (descending)
            specificationType?.GetMethod(
                    orderByDescendingMethodName,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                ?.Invoke(specification, new object[] { propertyReturningExpression });
        else
            specificationType?.GetMethod(
                    orderByMethodName,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                ?.Invoke(specification, new object[] { propertyReturningExpression });
    }
}