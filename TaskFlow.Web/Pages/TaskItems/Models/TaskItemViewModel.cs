using TaskFlow.Application.DTOs.TaskItems;
using TaskFlow.Core.Entities;

namespace TaskFlow.Web.Pages.TaskItems.Models;

public class TaskItemViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public TaskItemPriority Priority { get; set; }
    public TaskItemStatus Status { get; set; }
    public Guid? ProjectId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
