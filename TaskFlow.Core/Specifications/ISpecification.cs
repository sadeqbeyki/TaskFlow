using System.Linq.Expressions;

namespace TaskFlow.Core.Specifications;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy { get; }
    Func<IQueryable<T>, IOrderedQueryable<T>>? OrderByDescending { get; }
    int? Skip { get; }
    int? Take { get; }

    // Eager-loading includes (expressions)
    IReadOnlyList<Expression<Func<T, object>>> Includes { get; }
}
