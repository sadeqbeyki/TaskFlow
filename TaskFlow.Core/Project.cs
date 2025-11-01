using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Core;

public class Project
{
    // Parameterless Constructor for EF Core and object initializers

    public Project()
    {
        Tasks = new List<TaskItem>();
    }


    // Constructor for creating a new project in domain logic
    public Project(string title, string? description, Guid ownerId)
    {
        Title = string.IsNullOrWhiteSpace(title) ? throw new ArgumentException("Title cannot be empty.") : title.Trim();

        Description = description?.Trim();
        OwnerId = ownerId;
        CreatedAt = DateTime.UtcNow;
        Tasks = new List<TaskItem>();
    }

    // Properties
    public Guid Id { get; private set; } = Guid.NewGuid();

    [Required(ErrorMessage = "Project title is required.")]
    [StringLength(100, ErrorMessage = "Title must not exceed 100 characters.")]
    public string Title { get; private set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Description must not exceed 500 characters.")]
    public string? Description { get; private set; }

    public Guid OwnerId { get; private set; }

    [DataType(DataType.Date)]
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    // Navigation
    public User? Owner { get; private set; }
    public IReadOnlyCollection<TaskItem> Tasks { get; private set; } = new List<TaskItem>();


    // Domain Behaviors
    public void UpdateDetails(string title, string? description, Guid ownerId)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Project title cannot be empty.");

        Title = title.Trim();
        Description = description?.Trim();
        OwnerId = ownerId;
        UpdatedAt = DateTime.UtcNow;
    }

    public TaskItem AddTask(string title, string? description, DateTime? dueDate = null,
                            TaskItemPriority priority = TaskItemPriority.Medium)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Task title cannot be empty.");

        var task = new TaskItem(title, description, Id, dueDate, priority);
        var mutableTasks = Tasks.ToList();
        mutableTasks.Add(task);
        Tasks = mutableTasks.AsReadOnly();

        UpdatedAt = DateTime.UtcNow;
        return task;
    }

    public void RemoveTask(Guid taskId)
    {
        var mutableTasks = Tasks.ToList();
        var task = mutableTasks.FirstOrDefault(t => t.Id == taskId);
        if (task == null)
            throw new InvalidOperationException("Task not found.");

        mutableTasks.Remove(task);
        Tasks = mutableTasks.AsReadOnly();
        UpdatedAt = DateTime.UtcNow;
    }
}
