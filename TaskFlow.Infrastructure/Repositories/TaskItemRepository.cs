using Microsoft.EntityFrameworkCore;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Repositories;

namespace TaskFlow.Infrastructure.Repositories;

public class TaskItemRepository : GenericRepository<TaskItem>, ITaskItemRepository
{
    public TaskItemRepository(TaskFlowDbContext context)
        : base(context) { }

    public async Task<List<TaskItem>> GetByProjectAsync(Guid projectId)
    {
        return await _dbSet
            .Where(t => t.ProjectId == projectId)
            .ToListAsync();
    }

    public async Task<List<TaskItem>> GetByStatusAsync(TaskItemStatus status)
    {
        return await _dbSet
            .Where(t => t.Status == status)
            .ToListAsync();
    }
}
