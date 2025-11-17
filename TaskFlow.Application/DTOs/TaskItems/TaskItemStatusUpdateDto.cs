using TaskFlow.Core.Entities;

namespace TaskFlow.Application.DTOs.TaskItems;

// For changing the status of a task (e.g., MarkDone, MarkInProgress)
public class TaskItemStatusUpdateDto
{
    public TaskItemStatus Status { get; set; }
}

