using TaskFlow.Core.Entities;

namespace TaskFlow.Web.Pages.TaskItems.Models;

public class TaskItemViewModel
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public TaskItemPriority Priority { get; set; }
    public TaskItemStatus Status { get; set; }
    public Guid? ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
