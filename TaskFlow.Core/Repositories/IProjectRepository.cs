using TaskFlow.Core.Entities;

namespace TaskFlow.Core.Repositories;

public interface IProjectRepository : IGenericRepository<Project,Guid>
{
    Task<List<Project>> GetByOwnerAsync(Guid ownerId);
    Task<Project> GetProjectByIdAsync(Guid? id);
}
