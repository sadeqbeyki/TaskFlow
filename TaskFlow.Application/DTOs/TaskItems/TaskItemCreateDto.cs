using TaskFlow.Core.Entities;

namespace TaskFlow.Application.DTOs.TaskItems;

// For creating a new TaskItem
public class TaskItemCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public TaskItemPriority Priority { get; set; } = TaskItemPriority.Medium;

    // required: the project this task belongs to
    public Guid ProjectId { get; set; }
}