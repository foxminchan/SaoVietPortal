﻿using System.Linq.Expressions;
using Portal.Shared.Core.Interfaces;

namespace Portal.Shared.Core.Specifications;

public class And<T> : SpecificationBase<T>
{
    private readonly ISpecification<T> _left;
    private readonly ISpecification<T> _right;

    public And(
        ISpecification<T> left,
        ISpecification<T> right)
    {
        _left = left;
        _right = right;
    }

    public override Expression<Func<T, bool>> Filter
    {
        get
        {
            var parameter = Expression.Parameter(typeof(T), "obj");

            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(
                    Expression.Invoke(_left.Filter ?? throw new InvalidOperationException(), parameter),
                    Expression.Invoke(_right.Filter ?? throw new InvalidOperationException(), parameter)
                    ), parameter);
        }
    }
}