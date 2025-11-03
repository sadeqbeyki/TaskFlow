using TaskFlow.Core;

namespace TaskFlow.Application.DTOs.TaskItems;

// For updating an existing TaskItem
public class TaskItemUpdateDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public TaskItemPriority Priority { get; set; } = TaskItemPriority.Medium;
}

