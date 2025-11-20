using TaskFlow.Application.DTOs.Projects;
using TaskFlow.Application.Interfaces;
using TaskFlow.Application.Mappers;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Repositories;

namespace TaskFlow.Application.Services;

public class ProjectService : IProjectService
{
    private readonly IGenericRepository<Project, Guid> _genericRepository;
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IGenericRepository<Project, Guid> genericRepository, IProjectRepository projectRepository)
    {
        _genericRepository = genericRepository;
        _projectRepository = projectRepository;
    }

    public async Task<ProjectDto?> GetByIdAsync(Guid id, Guid ownerId)
    {
        var project = await _genericRepository.GetByIdAsync(id);

        return project is null
                ? null
                : ProjectMapper.MapToDto(project);
    }

    public async Task<List<ProjectDto>> GetAllByUserAsync(Guid ownerId)
    {
        var projects = await _projectRepository.GetByOwnerAsync(ownerId);
        return projects
            .Select(ProjectMapper.MapToDto)!
            .ToList();

    }

    public async Task<Guid> CreateAsync(ProjectCreateDto dto, Guid ownerId)
    {
        var project = new Project(dto.Title.Trim(), dto.Description, ownerId);

        await _genericRepository.AddAsync(project);

        return project.Id;
    }

    public async Task<bool> UpdateAsync(Guid projectId, ProjectUpdateDto dto, Guid ownerId)
    {
        var project = await _genericRepository.GetByIdAsync(projectId);

        if (project is null)
            return false;

        project.UpdateDetails(dto.Title.Trim(), dto.Description);

        await _genericRepository.UpdateAsync(project);

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, Guid ownerId)
    {
        var project = await _genericRepository.GetByIdAsync(id);
        if (project is null)
            return false;

        _genericRepository.Remove(project);

        return true;
    }
}
