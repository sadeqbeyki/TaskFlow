using TaskFlow.Core.Entities;

namespace TaskFlow.Application.Interfaces;

public interface IProjectRepository
{
    Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Project>> GetAllAsync();
    void Add(Project project);
    void Remove(Project project);
}
