using TaskFlow.Core.Entities;

namespace TaskFlow.Application.DTOs.TaskItems;

public class TaskItemUpdateDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public TaskItemPriority Priority { get; set; }
    public Guid? ProjectId { get; set; }
}

