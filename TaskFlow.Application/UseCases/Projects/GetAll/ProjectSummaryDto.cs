namespace TaskFlow.Application.UseCases.Projects.GetAll;

public sealed class ProjectSummaryDto
{
    public Guid Id { get; }
    public string Title { get; }
    public DateTime CreatedAt { get; }

    public ProjectSummaryDto(
        Guid id,
        string title,
        DateTime createdAt)
    {
        Id = id;
        Title = title;
        CreatedAt = createdAt;
    }
}