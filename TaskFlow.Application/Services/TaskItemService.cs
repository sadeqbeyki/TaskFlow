using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.DTOs.TaskItems;
using TaskFlow.Application.Interfaces;
using TaskFlow.Application.Mappers;
using TaskFlow.Core.Entities;
using TaskFlow.Infrastructure;

namespace TaskFlow.Application.Services;

public class TaskItemService : ITaskItemService
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

        return TaskItemMapper.MapToDto(task);
    }

    public async Task<List<TaskItemDto>> GetAllByProjectAsync(Guid projectId, Guid ownerId)
    {
        // Validate project ownership (prevents exposing tasks of other users)
        var project = await _context.Projects
            .AsNoTracking()
            .Include(x => x.Tasks)
            .FirstOrDefaultAsync(p => p.Id == projectId && p.OwnerId == ownerId);

        if (project == null) return new List<TaskItemDto>();
        if (project.OwnerId != ownerId) return new List<TaskItemDto>();

        //var tasks = await _context.TaskItems
        //    .AsNoTracking()
        //    .Where(t => t.ProjectId == projectId)
        //    .OrderByDescending(t => t.CreatedAt)
        //    .ToListAsync();

        //return tasks.Select(TaskItemMapper.MapToDto).ToList();


        return project.Tasks!
            .Select(TaskItemMapper.MapToDto)
            .ToList();
    }

    public async Task<Guid> CreateAsync(TaskItemCreateDto dto, Guid ownerId)
    {
        // Validate project exists and belongs to owner
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == dto.ProjectId && p.OwnerId == ownerId)
            ?? throw new InvalidOperationException("Project not found or you do not have permission.");

        // Create domain entity using constructor (DDD-friendly)
        var task = new TaskItem(
            title: dto.Title.Trim(),
            description: dto.Description,
            projectId: dto.ProjectId,
            dueDate: dto.DueDate,
            priority: dto.Priority
        );

        _context.TaskItems.Add(task);
        await _context.SaveChangesAsync();

        return task.Id;
    }

    public async Task<bool> UpdateAsync(Guid id, TaskItemUpdateDto dto, Guid ownerId)
    {
        var task = await _context.TaskItems
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (task == null) return false;
        if (task.Project != null && task.Project.OwnerId != ownerId) return false; // not authorized

        // Use domain behavior to update fields
        task.UpdateDetails(dto.Title.Trim(), dto.Description, dto.DueDate, dto.Priority);

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, Guid ownerId)
    {
        var task = await _context.TaskItems
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (task == null) return false;
        if (task.Project != null && task.Project.OwnerId != ownerId) return false; // not authorized

        _context.TaskItems.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ChangeStatusAsync(Guid id, TaskItemStatusUpdateDto dto, Guid ownerId)
    {
        var task = await _context.TaskItems
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (task == null) return false;
        if (task.Project != null && task.Project.OwnerId != ownerId) return false; // not authorized

        // Use domain methods to change status (keeps rules inside entity)
        switch (dto.Status)
        {
            case TaskItemStatus.Todo:
                // Reopen expects it was Done
                if (task.Status == TaskItemStatus.Done)
                    task.Reopen();
                else
                    task.UpdateDetails(task.Title, task.Description, task.DueDate, task.Priority); // no-op but update timestamp? (we keep no-op)
                break;
            case TaskItemStatus.InProgress:
                task.MarkInProgress();
                break;
            case TaskItemStatus.Done:
                task.MarkDone();
                break;
            default:
                break;
        }

        await _context.SaveChangesAsync();
        return true;
    }

    // ---------------------------------------------------------
    // Status changes
    // ---------------------------------------------------------
    public async Task<bool> MarkInProgressAsync(Guid id, Guid ownerId)
    {
        var task = await _context.TaskItems.Include(t => t.Project).FirstOrDefaultAsync(t => t.Id == id);
        if (task == null || task.Project?.OwnerId != ownerId) return false;

        task.MarkInProgress();
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> MarkDoneAsync(Guid id, Guid ownerId)
    {
        var task = await _context.TaskItems.Include(t => t.Project).FirstOrDefaultAsync(t => t.Id == id);
        if (task == null || task.Project?.OwnerId != ownerId) return false;

        task.MarkDone();
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ReopenAsync(Guid id, Guid ownerId)
    {
        var task = await _context.TaskItems.Include(t => t.Project).FirstOrDefaultAsync(t => t.Id == id);
        if (task == null || task.Project?.OwnerId != ownerId) return false;

        task.Reopen();
        await _context.SaveChangesAsync();
        return true;
    }
}
