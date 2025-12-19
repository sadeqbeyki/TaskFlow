using TaskFlow.Core.ValueObjects;

namespace TaskFlow.Core.Entities;

public class Project
{
    // Parameterless Constructor for EF Core and object initializers

    public Project()
    {
        Tasks = new List<TaskItem>();
    }


    // Constructor for creating a new project in domain logic
    public Project(ProjectTitle title, string? description, Guid ownerId)
    {
        Title = title;

        Description = description?.Trim();
        OwnerId = ownerId;
        CreatedAt = DateTime.UtcNow;
        Tasks = new List<TaskItem>();
    }

    // Properties
    public Guid Id { get; private set; } = Guid.NewGuid();

    public ProjectTitle Title { get; private set; }

    public string? Description { get; private set; }


    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public Guid? OwnerId { get; private set; }

    // Navigation
    public User? Owner { get; private set; }
    public IReadOnlyCollection<TaskItem> Tasks { get; private set; } = new List<TaskItem>();


    // Domain Behaviors
    public void UpdateDetails(ProjectTitle title, string? description)
    {
        Title = title;
        Description = description?.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    public TaskItem AddTask(TaskTitle title, string? description, DateTime? dueDate = null,
                            TaskItemPriority priority = TaskItemPriority.Medium)
    {
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
