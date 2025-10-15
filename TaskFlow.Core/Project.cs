namespace TaskFlow.Core;

public class Project
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public Guid OwnerId { get; set; }


    // Navigation
    public User? Owner { get; set; }
    public ICollection<TaskItem>? Tasks { get; set; }
}



