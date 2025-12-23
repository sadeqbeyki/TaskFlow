namespace TaskFlow.Application.UseCases.Projects.GetById;

public sealed class ProjectDetailsDto
{
    public Guid Id { get; }
    public string Title { get; }
    public string? Description { get; }
    public DateTime CreatedAt { get; }

    public ProjectDetailsDto(
        Guid id,
        string title,
        string? description,
        DateTime createdAt)
    {
        Id = id;
        Title = title;
        Description = description;
        CreatedAt = createdAt;
    }
}
