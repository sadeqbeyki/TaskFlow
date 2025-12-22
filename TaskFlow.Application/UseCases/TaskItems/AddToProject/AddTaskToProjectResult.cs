

namespace TaskFlow.Application.UseCases.TaskItems.AddToProject;

public sealed class AddTaskToProjectResult
{
    public Guid TaskId { get; }

    public AddTaskToProjectResult(Guid taskId)
    {
        TaskId = taskId;
    }
}
