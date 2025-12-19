using TaskFlow.Core.Entities;
using TaskFlow.Core.ValueObjects;

namespace TaskFlow.Core.Factories;

public static class TaskItemFactory
{
    public static TaskItem Create(
        string title,
        string? description,
        Guid projectId,
        DateTime? dueDate,
        TaskItemPriority priority = TaskItemPriority.Medium)
    {
        if (dueDate.HasValue && dueDate.Value.Date < DateTime.UtcNow.Date)
            throw new ArgumentException("Due date cannot be in the past.");

        var taskTitle = new TaskTitle(title);

        return new TaskItem(
            title: taskTitle,
            description: description,
            projectId: projectId,
            dueDate: dueDate,
            priority: priority
        );
    }
}

