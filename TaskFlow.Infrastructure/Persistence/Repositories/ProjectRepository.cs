using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.Interfaces;
using TaskFlow.Core.Entities;

namespace TaskFlow.Infrastructure.Persistence.Repositories;

public sealed class ProjectRepository : IProjectRepository
{
    private readonly TaskFlowDbContext _context;

    public ProjectRepository(TaskFlowDbContext context)
    {
        _context = context;
    }

    public async Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Projects
            .Include(p => p.Tasks)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<IReadOnlyList<Project>> GetAllAsync()
    {
        return await _context.Projects
            .AsNoTracking()
            .ToListAsync();
    }
    public void Add(Project project)
    {
        _context.Projects.Add(project);
    }
    public void Remove(Project project)
    {
        _context.Projects.Remove(project);
    }

}
