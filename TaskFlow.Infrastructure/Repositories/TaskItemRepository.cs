using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.DTOs.TaskItems;
using TaskFlow.Application.Specifications;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Filters;
using TaskFlow.Core.Repositories;
using TaskFlow.Core.Specifications;

namespace TaskFlow.Infrastructure.Repositories;

public class TaskItemRepository : GenericRepository<TaskItem, Guid>, ITaskItemRepository
{
    public TaskItemRepository(TaskFlowDbContext context)
        : base(context) { }

    public async Task<bool> ChangeStatusAsync(Guid id, TaskItemStatus status, Guid ownerId)
    {
        var task = await _context.TaskItems
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (task == null) return false;
        if (task.Project != null && task.Project.OwnerId != ownerId) return false; // not authorized

        // Use domain methods to change status (keeps rules inside entity)
        switch (status)
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

    public async Task<bool> ValidateProjectOwnerAsync(Guid projectId, Guid ownerId)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == projectId && p.OwnerId == ownerId) //.AnyAsync(p => p.Id == projectId && p.OwnerId == ownerId);
            ?? throw new InvalidOperationException("Project not found or you do not have permission.");
        if (project == null)
            return false;
        return true;
    }


    public async Task<List<TaskItem>> ListAsync(ISpecification<TaskItem> spec, CancellationToken cancellationToken = default)
    {
        var query = ApplySpecification(_context.TaskItems.AsQueryable(), spec);
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync(ISpecification<TaskItem> spec, CancellationToken cancellationToken = default)
    {
        var query = ApplySpecification(_context.TaskItems.AsQueryable(), spec, ignorePaging: true);
        return await query.CountAsync(cancellationToken);
    }

    private IQueryable<TaskItem> ApplySpecification(IQueryable<TaskItem> query, ISpecification<TaskItem> spec, bool ignorePaging = false)
    {
        // Criteria
        if (spec.Criteria != null)
            query = query.Where(spec.Criteria);

        // Includes
        foreach (var include in spec.Includes)
            query = query.Include(include);

        // Sorting
        if (spec.OrderBy != null)
            query = spec.OrderBy(query);
        else if (spec.OrderByDescending != null)
            query = spec.OrderByDescending(query);

        // Paging
        if (!ignorePaging)
        {
            if (spec.Skip.HasValue)
                query = query.Skip(spec.Skip.Value);
            if (spec.Take.HasValue)
                query = query.Take(spec.Take.Value);
        }

        return query;
    }

}
