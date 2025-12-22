using TaskFlow.Core.Entities;

namespace TaskFlow.Application.UseCases.TaskItems.AddToProject;

public sealed class AddTaskToProjectCommand
{
    public Guid ProjectId { get; }
    public string Title { get; }
    public string? Description { get; }
    public DateTime? DueDate { get; }
    public TaskItemPriority Priority { get; }

    public AddTaskToProjectCommand(
        Guid projectId,
        string title,
        string? description,
        DateTime? dueDate,
        TaskItemPriority priority = TaskItemPriority.Medium)
    {
        ProjectId = projectId;
        Title = title;
        Description = description;
        DueDate = dueDate;
        Priority = priority;
    }
}
