namespace TaskFlow.Application.UseCases.Projects.Delete;

public sealed class DeleteProjectResult
{
    public Guid ProjectId { get; }

    public DeleteProjectResult(Guid projectId)
    {
        ProjectId = projectId;
    }
}