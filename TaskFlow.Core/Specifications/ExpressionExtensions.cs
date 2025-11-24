using System.Linq.Expressions;

namespace TaskFlow.Core.Specifications;

public static class ExpressionExtensions
{
    public static Expression<Func<T, bool>> AndAlso<T>(
        this Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right)
    {
        if (left == null) return right;

        var param = Expression.Parameter(typeof(T));

        var body = Expression.AndAlso(
            Expression.Invoke(left, param),
            Expression.Invoke(right, param));

        return Expression.Lambda<Func<T, bool>>(body, param);
    }
}