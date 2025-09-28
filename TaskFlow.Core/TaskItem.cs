using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Core;

public enum TaskItemPriority { Low = 0, Medium = 1, High = 2 }
public enum TaskItemStatus { Todo = 0, InProgress = 1, Done = 2 }
public class TaskItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(100, ErrorMessage = "Title must not exceed 100 characters.")]
    public string Title { get; set; } = null!;
    [StringLength(500, ErrorMessage = "Description must not exceed 500 characters.")]
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public TaskItemPriority Priority { get; set; } = TaskItemPriority.Medium;
    public TaskItemStatus Status { get; set; } = TaskItemStatus.Todo;


    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }


    // Audit
    [DataType(DataType.Date)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}