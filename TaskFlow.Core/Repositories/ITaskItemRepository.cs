using TaskFlow.Core.Entities;

namespace TaskFlow.Core.Repositories
{
    public interface ITaskItemRepository : IRepository<TaskItem>
    {
        Task<List<TaskItem>> GetByProjectAsync(Guid projectId);
        Task<List<TaskItem>> GetByStatusAsync(TaskItemStatus status);
    }
}
