namespace TaskFlow.Core;

public enum TaskPriority { Low = 0, Medium = 1, High = 2 }
public enum TaskStatus { Todo = 0, InProgress = 1, Done = 2 }
public class TaskItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public TaskStatus Status { get; set; } = TaskStatus.Todo;


    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }


    // Audit
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}