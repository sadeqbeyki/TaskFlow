using System.Linq.Expressions;

namespace TaskFlow.Core.Specifications
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> AndAlso<T>(
            this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right)
        {
            if (left == null) return right ?? (t => true);
            if (right == null) return left;

            // Get the expressions
            var leftExpr = left;
            var rightExpr = right;

            // Create a single parameter to replace both parameters
            var parameter = Expression.Parameter(typeof(T), "x");

            // Rebind parameters
            var leftBody = ExpressionRebinder.Replace(leftExpr.Parameters[0], parameter, leftExpr.Body);
            var rightBody = ExpressionRebinder.Replace(rightExpr.Parameters[0], parameter, rightExpr.Body);

            var body = Expression.AndAlso(leftBody, rightBody);
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        public static Expression<Func<T, bool>> OrElse<T>(
            this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right)
        {
            if (left == null) return right ?? (t => false);
            if (right == null) return left;

            var leftExpr = left;
            var rightExpr = right;
            var parameter = Expression.Parameter(typeof(T), "x");

            var leftBody = ExpressionRebinder.Replace(leftExpr.Parameters[0], parameter, leftExpr.Body);
            var rightBody = ExpressionRebinder.Replace(rightExpr.Parameters[0], parameter, rightExpr.Body);

            var body = Expression.OrElse(leftBody, rightBody);
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}

