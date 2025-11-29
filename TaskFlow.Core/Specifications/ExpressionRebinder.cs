using System.Linq.Expressions;

namespace TaskFlow.Core.Specifications;

internal class ExpressionRebinder : ExpressionVisitor
{
    private readonly ParameterExpression _oldParam;
    private readonly ParameterExpression _newParam;

    public ExpressionRebinder(ParameterExpression oldParam, ParameterExpression newParam)
    {
        _oldParam = oldParam;
        _newParam = newParam;
    }

    public static Expression Replace(ParameterExpression oldParam, ParameterExpression newParam, Expression expr)
        => new ExpressionRebinder(oldParam, newParam).Visit(expr);

    protected override Expression VisitParameter(ParameterExpression node)
        => node == _oldParam ? _newParam : node;
}
