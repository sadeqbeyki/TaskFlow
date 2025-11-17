namespace TaskFlow.Core.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserName { get; set; } = null!;
    public string? FullName { get; set; }
    public string? Email { get; set; }


    // Navigation
    public ICollection<Project>? Projects { get; set; }
}


