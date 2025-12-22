namespace TaskFlow.Application.UseCases.Projects.Create
{
    public sealed class CreateProjectCommand
    {
        public string Title { get; init; } = string.Empty;
        public string? Description { get; init; }
        public Guid OwnerId { get; init; }
    }
}
