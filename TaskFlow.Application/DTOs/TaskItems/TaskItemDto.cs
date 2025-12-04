using TaskFlow.Core.Entities;

namespace TaskFlow.Application.DTOs.TaskItems;

public class TaskItemDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public TaskItemPriority Priority { get; set; }
    public TaskItemStatus Status { get; set; }
    public Guid? ProjectId { get; set; }
    public string ProjectTitle { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
