using TaskFlow.Core.Entities;

namespace TaskFlow.Application.DTOs.TaskItems;

public class TaskItemCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public TaskItemPriority Priority { get; set; } = TaskItemPriority.Medium;
    public Guid ProjectId { get; set; }
}