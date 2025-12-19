using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Repositories;
using TaskFlow.Core.Specifications;
using TaskFlow.Infrastructure.Persistence;

namespace TaskFlow.Infrastructure.Repositories;

public class TaskItemRepository(TaskFlowDbContext context) : GenericRepository<TaskItem, Guid>(context), ITaskItemRepository
{
    public async Task<TaskItem?> GetWithProjectAsync(Guid id, Guid ownerId)
    {
        return await _context.TaskItems
            .Include(p => p.Project)
            .FirstOrDefaultAsync(t => t.Id == id);

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

    // Begin Filter
    public async Task<List<TResult>> ListAsync<TResult>(ISpecification<TaskItem> spec, Expression<Func<TaskItem, TResult>> selector, CancellationToken cancellationToken = default)
    {
        var query = ApplySpecification(_context.TaskItems.AsQueryable(), spec);
        return await query.Select(selector).ToListAsync(cancellationToken);
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

    // End Filter

    public async Task SaveAsync() => await _context.SaveChangesAsync();


}
