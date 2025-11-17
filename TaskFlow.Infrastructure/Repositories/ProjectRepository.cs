using Microsoft.EntityFrameworkCore;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Repositories;

namespace TaskFlow.Infrastructure.Repositories;

public class ProjectRepository : Repository<Project>, IProjectRepository
{
    public ProjectRepository(TaskFlowDbContext context)
        : base(context) { }

    public async Task<List<Project>> GetByOwnerAsync(Guid ownerId)
    {
        return await _db
            .Where(p => p.OwnerId == ownerId)
            .Include(p => p.Tasks)
            .ToListAsync();
    }
}
