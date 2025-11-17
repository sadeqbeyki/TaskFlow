using TaskFlow.Core.Entities;

namespace TaskFlow.Core.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<List<Project>> GetByOwnerAsync(Guid ownerId);
    }
}
