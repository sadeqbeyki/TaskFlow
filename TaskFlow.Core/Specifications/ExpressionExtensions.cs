using System.Linq.Expressions;

namespace TaskFlow.Core.Specifications;

public static class ExpressionExtensions
{
    public static Expression<Func<T, bool>> AndAlso<T>(
        this Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right)
    {
        if (left == null) return right ?? (t => true);
        if (right == null) return left;

        var param = Expression.Parameter(typeof(T));
        var leftInvoked = Expression.Invoke(left, param);
        var rightInvoked = Expression.Invoke(right, param);
        var body = Expression.AndAlso(leftInvoked, rightInvoked);
        return Expression.Lambda<Func<T, bool>>(body, param);
    }
}