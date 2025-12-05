using TaskFlow.Core.Entities;

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
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.");

        if (dueDate.HasValue && dueDate.Value.Date < DateTime.UtcNow.Date)
            throw new ArgumentException("Due date cannot be in the past.");


        return new TaskItem(
            title: title.Trim(),
            description: description,
            projectId: projectId,
            dueDate: dueDate,
            priority: priority
        );
    }
}

