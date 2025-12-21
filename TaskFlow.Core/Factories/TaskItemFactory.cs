using TaskFlow.Core.Entities;
using TaskFlow.Core.ValueObjects;

namespace TaskFlow.Core.Factories;

internal static class TaskItemFactory
{
    internal static TaskItem Create(
        TaskTitle title,
        string? description,
        Guid projectId,
        DateTime? dueDate,
        TaskItemPriority priority = TaskItemPriority.Medium)
    {
        if (dueDate.HasValue && dueDate.Value.Date < DateTime.UtcNow.Date)
            throw new ArgumentException("Due date cannot be in the past.");

        return new TaskItem(
            title: title,
            description: description,
            projectId: projectId,
            dueDate: dueDate,
            priority: priority
        );
    }
}

