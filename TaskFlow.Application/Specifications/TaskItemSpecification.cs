using System.Linq.Expressions;
using TaskFlow.Application.Filters;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Specifications;

namespace TaskFlow.Application.Specifications;

public sealed class TaskItemSpecification : ISpecification<TaskItem>
{
    public Expression<Func<TaskItem, bool>>? Criteria { get; }
    public Func<IQueryable<TaskItem>, IOrderedQueryable<TaskItem>>? OrderBy { get; }
    public int? Skip { get; }
    public int? Take { get; }

    public TaskItemSpecification(TaskItemFilter filter)
    {
        Criteria = BuildCriteria(filter);

        OrderBy = filter.OrderBy?.ToLower() switch
        {
            "created" => (q => filter.Desc
                                ? q.OrderByDescending(t => t.CreatedAt)
                                : q.OrderBy(t => t.CreatedAt)),
            "due" => (q => filter.Desc
                                ? q.OrderByDescending(t => t.DueDate)
                                : q.OrderBy(t => t.DueDate)),
            _ => null
        };

        Skip = (filter.Page - 1) * filter.PageSize;
        Take = filter.PageSize;
    }

    private static Expression<Func<TaskItem, bool>> BuildCriteria(TaskItemFilter f)
    {
        Expression<Func<TaskItem, bool>> expr = t => true;

        if (!string.IsNullOrWhiteSpace(f.Search))
        {
            Expression<Func<TaskItem, bool>> s =
                t => t.Title.Contains(f.Search)
                  || (t.Description != null && t.Description.Contains(f.Search));

            expr = expr.AndAlso(s);
        }


        if (f.CreatedFrom != null)
            expr = expr.AndAlso(t => t.CreatedAt >= f.CreatedFrom.Value);

        if (f.CreatedTo != null)
            expr = expr.AndAlso(t => t.CreatedAt <= f.CreatedTo.Value);

        if (f.DueFrom != null)
            expr = expr.AndAlso(t => t.DueDate >= f.DueFrom.Value);

        if (f.DueTo != null)
            expr = expr.AndAlso(t => t.DueDate <= f.DueTo.Value);

        return expr;
    }
}