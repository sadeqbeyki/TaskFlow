namespace TaskFlow.Application.UseCases.Projects.Update;

public sealed class UpdateProjectResult
{
    public Guid ProjectId { get; }

    public UpdateProjectResult(Guid projectId)
    {
        ProjectId = projectId;
    }
}