using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.DTOs.TaskItems;
using TaskFlow.Application.Mappers;
using TaskFlow.Infrastructure;

namespace TaskFlow.Application.Services;

public class TaskItemService
{
    private readonly TaskFlowDbContext _context;

    public TaskItemService(TaskFlowDbContext context)
    {
        _context = context;
    }

    public async Task<TaskItemDto?> GetByIdAsync(Guid id, Guid ownerId)
    {
        // Ensure the task exists and belongs to a project owned by ownerId (authorization)
        var task = await _context.TaskItems
            .AsNoTracking()
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (task == null) return null;
        if (task.Project != null && task.Project.OwnerId != ownerId) return null; // not authorized

        return TaskItemMapper.ToDto(task);
    }
}
