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

    public async Task<List<TaskItemDto>> GetAllByProjectAsync(Guid projectId, Guid ownerId)
    {
        // Validate project ownership (prevents exposing tasks of other users)
        var project = await _context.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == projectId && p.OwnerId == ownerId);

        if (project == null) return new List<TaskItemDto>();

        var tasks = await _context.TaskItems
            .AsNoTracking()
            .Where(t => t.ProjectId == projectId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();

        return tasks.Select(TaskItemMapper.ToDto).ToList();
    }

}
