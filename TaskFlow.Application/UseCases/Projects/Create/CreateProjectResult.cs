
namespace TaskFlow.Application.UseCases.Projects.Create
{
    public sealed class CreateProjectResult
    {
        public Guid ProjectId { get; }

        public CreateProjectResult(Guid projectId)
        {
            ProjectId = projectId;
        }
    }
}
