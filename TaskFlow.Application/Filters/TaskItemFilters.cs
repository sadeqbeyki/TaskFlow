using TaskFlow.Core.Entities;

namespace TaskFlow.Application.Filters;

public class TaskItemFilter
{
    public string? SearchText { get; set; }
    public TaskItemStatus? Status { get; set; }
    public TaskItemPriority? Priority { get; set; }
    public Guid? ProjectId { get; set; }
}



