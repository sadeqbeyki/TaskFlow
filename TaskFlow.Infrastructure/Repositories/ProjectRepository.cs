using Microsoft.EntityFrameworkCore;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Repositories;

namespace TaskFlow.Infrastructure.Repositories;

public class ProjectRepository : GenericRepository<Project, Guid>, IProjectRepository
{
    public ProjectRepository(TaskFlowDbContext context)
        : base(context) { }

    public async Task<List<Project>> GetByOwnerAsync(Guid ownerId)
    {
        return await _dbSet
            .AsNoTracking()//
            .Where(p => p.OwnerId == ownerId)
            .Include(p => p.Tasks)
            .ToListAsync();
    }

    public async Task<Project> GetProjectByIdAsync(Guid? id)
    {
        return await _dbSet.FindAsync(id); // FindAsync = Tracking ON
    }


}
