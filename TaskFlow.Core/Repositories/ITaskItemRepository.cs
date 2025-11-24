using TaskFlow.Application.Filters;
using TaskFlow.Core.Entities;

namespace TaskFlow.Core.Repositories
{
    public interface ITaskItemRepository : IGenericRepository<TaskItem, Guid>
    {
        Task<bool> ChangeStatusAsync(Guid id, TaskItemStatus status, Guid ownerId);
        Task<List<TaskItem>> GetByProjectAsync(Guid projectId);

        Task<List<TaskItem>> GetByStatusAsync(TaskItemStatus status);
        Task<bool> MarkDoneAsync(Guid id, Guid ownerId);
        Task<bool> MarkInProgressAsync(Guid id, Guid ownerId);
        Task<bool> ReopenAsync(Guid id, Guid ownerId);

        Task<bool> ValidateProjectOwnerAsync(Guid projectId, Guid ownerId);


    }
}
