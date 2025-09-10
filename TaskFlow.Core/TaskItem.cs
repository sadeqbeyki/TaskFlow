namespace TaskFlow.Core;

public enum TaskItemPriority { Low = 0, Medium = 1, High = 2 }
public enum TaskItemStatus { Todo = 0, InProgress = 1, Done = 2 }
public class TaskItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public TaskItemPriority Priority { get; set; } = TaskItemPriority.Medium;
    public TaskItemStatus Status { get; set; } = TaskItemStatus.Todo;


    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }


    // Audit
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}