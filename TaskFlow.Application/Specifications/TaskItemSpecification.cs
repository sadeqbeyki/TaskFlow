using System.Linq.Expressions;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Filters;
using TaskFlow.Core.Specifications;

namespace TaskFlow.Application.Specifications;

public sealed class TaskItemSpecification : BaseSpecification<TaskItem>
{
    public TaskItemSpecification(TaskItemFilter filter)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));

        // Search
        if (!string.IsNullOrWhiteSpace(filter.SearchText))
        {
            var search = filter.SearchText.Trim();
            if (filter.SearchInDescription)
            {
                AddCriteria(t =>
                    t.Title.Contains(search) ||
                    (t.Description != null && t.Description.Contains(search))
                );
            }
            else
            {
                AddCriteria(t => t.Title != null && t.Title.Contains(search));
            }
        }

        if (filter.Priority.HasValue)
            AddCriteria(t => t.Priority == filter.Priority.Value);
        if (filter.Status.HasValue)
            AddCriteria(t => t.Status == filter.Status.Value);
        if (filter.ProjectId.HasValue)
            AddCriteria(t => t.ProjectId == filter.ProjectId.Value);

        if (filter.DueDateFrom.HasValue)
            AddCriteria(t => t.DueDate.HasValue && t.DueDate >= filter.DueDateFrom.Value);
        if (filter.DueDateTo.HasValue)
            AddCriteria(t => t.DueDate.HasValue && t.DueDate <= filter.DueDateTo.Value);
        if (filter.CreatedFrom.HasValue)
            AddCriteria(t => t.CreatedAt >= filter.CreatedFrom.Value);
        if (filter.CreatedTo.HasValue)
            AddCriteria(t => t.CreatedAt <= filter.CreatedTo.Value);

        // Sorting
        var sort = filter.SortBy?.Trim().ToLower();
        switch (sort)
        {
            case "title":
                ApplySorting(filter.SortDescending, t => t.Title);
                break;
            case "duedate":
                ApplySorting(filter.SortDescending, t => t.DueDate);
                break;
            case "priority":
                ApplySorting(filter.SortDescending, t => t.Priority);
                break;
            case "status":
                ApplySorting(filter.SortDescending, t => t.Status);
                break;
            case "createdat":
            case "created":
                ApplySorting(filter.SortDescending, t => t.CreatedAt);
                break;
            default:
                ApplyOrderBy(q => q.OrderBy(t => t.CreatedAt));
                break;
        }

        // Paging
        var pageNumber = Math.Max(1, filter.PageNumber);
        var pageSize = Math.Max(1, filter.PageSize);
        ApplyPaging((pageNumber - 1) * pageSize, pageSize);
    }

    private void ApplySorting<TKey>(bool descending, Expression<Func<TaskItem, TKey>> keySelector)
    {
        if (descending)
            ApplyOrderByDescending(q => q.OrderByDescending(keySelector));
        else
            ApplyOrderBy(q => q.OrderBy(keySelector));
    }
}