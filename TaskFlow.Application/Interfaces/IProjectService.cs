using TaskFlow.Application.DTOs.Projects;

namespace TaskFlow.Application.Interfaces;

public interface IProjectService
{
    Task<ProjectDto?> GetByIdAsync(Guid id);
    Task<List<ProjectDto>> GetAllByUserAsync(Guid ownerId);

    Task<Guid> CreateAsync(ProjectCreateDto dto, Guid ownerId);
    Task<bool> UpdateAsync(Guid id, ProjectUpdateDto dto, Guid ownerId);
    Task<bool> DeleteAsync(Guid id, Guid ownerId);
}
