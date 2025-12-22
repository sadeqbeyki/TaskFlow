using TaskFlow.Core.Entities;

namespace TaskFlow.Application.Interfaces
{
    public interface IProjectRepository
    {
        void Add(Project project);
        Task<Project?> GetByIdAsync(Guid id);
    }
}
