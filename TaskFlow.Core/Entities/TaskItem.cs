using System.ComponentModel.DataAnnotations;
using TaskFlow.Core.ValueObjects;

namespace TaskFlow.Core.Entities;

public enum TaskItemPriority
{
    Low = 0,
    Medium = 1,
    High = 2
}

public enum TaskItemStatus
{
    Todo = 0,
    InProgress = 1,
    Done = 2
}

public class TaskItem
{
    private TaskItem() { }  

    internal TaskItem(
    TaskTitle title,
    string? description,
    Guid projectId,
    DateTime? dueDate,
    TaskItemPriority priority)
    {
        Title = title;
        Description = description?.Trim();
        ProjectId = projectId;
        DueDate = dueDate;
        Priority = priority;
        Status = TaskItemStatus.Todo;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; } = Guid.NewGuid();
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(100, ErrorMessage = "The title should not exceed 100 characters.")]
    public TaskTitle Title { get; private set; }
    [StringLength(500, ErrorMessage = "Descriptions should not exceed 500 characters.")]
    public string? Description { get; private set; }
    public DateTime? DueDate { get; private set; }

    public TaskItemPriority Priority { get; private set; }
    public TaskItemStatus Status { get; private set; }

    public Guid ProjectId { get; private set; }
    public Project Project { get; private set; } = default!;

    [DataType(DataType.Date)]
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    // Domain Behaviors
    public void UpdateDetails(
        TaskTitle title, 
        string? description, 
        DateTime? dueDate, 
        TaskItemPriority priority, 
        Guid projectId)
    {
        Title = title;
        Description = description?.Trim();
        DueDate = dueDate;
        Priority = priority;
        ProjectId = projectId;

        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkInProgress()
    {
        if (Status == TaskItemStatus.Done)
            throw new InvalidOperationException("A task that has been completed cannot be started again.");

        Status = TaskItemStatus.InProgress;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkDone()
    {
        if (Status == TaskItemStatus.Done)
            return;

        Status = TaskItemStatus.Done;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reopen()
    {
        if (Status != TaskItemStatus.Done)
            throw new InvalidOperationException("Only completed tasks can be reopened.");

        Status = TaskItemStatus.Todo;
        UpdatedAt = DateTime.UtcNow;
    }
}
