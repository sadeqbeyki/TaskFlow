using System.Linq.Expressions;

namespace TaskFlow.Core.Specifications;

public abstract class BaseSpecification<T> : ISpecification<T>
{
    private readonly List<Expression<Func<T, object>>> _includes = new();
    private Expression<Func<T, bool>>? _criteria;
    private Func<IQueryable<T>, IOrderedQueryable<T>>? _orderBy;
    private Func<IQueryable<T>, IOrderedQueryable<T>>? _orderByDesc;
    private int? _skip;
    private int? _take;

    public Expression<Func<T, bool>>? Criteria => _criteria;
    public Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy => _orderBy;
    public Func<IQueryable<T>, IOrderedQueryable<T>>? OrderByDescending => _orderByDesc;
    public int? Skip => _skip;
    public int? Take => _take;
    public IReadOnlyList<Expression<Func<T, object>>> Includes => _includes.AsReadOnly();

    protected void AddCriteria(Expression<Func<T, bool>> criteria)
        => _criteria = _criteria == null ? criteria : _criteria.AndAlso(criteria);

    protected void AddInclude(Expression<Func<T, object>> includeExpression) => _includes.Add(includeExpression);
    protected void ApplyOrderBy(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy) => _orderBy = orderBy;
    protected void ApplyOrderByDescending(Func<IQueryable<T>, IOrderedQueryable<T>> orderByDesc) => _orderByDesc = orderByDesc;
    protected void ApplyPaging(int skip, int take)
    {
        _skip = skip;
        _take = take;
    }
}