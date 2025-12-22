namespace TaskFlow.Application.UseCases.Projects.Create;

public sealed class CreateProjectCommand
{
    public string Title { get; }
    public string? Description { get; }
    public Guid OwnerId { get; }

    public CreateProjectCommand(string title, string? description, Guid ownerId)
    {
        Title = title;
        Description = description;
        OwnerId = ownerId;
    }
}
