using TaskFlow.Application.DTOs.Projects;
using TaskFlow.Application.Interfaces;
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

    //private readonly TaskFlowDbContext _context;

    //public ProjectService(TaskFlowDbContext context)
    //{
    //    _context = context;
    //}

    public async Task<ProjectDto?> GetByIdAsync(Guid id)
    {
        var project = await GetByIdAsync(id);

        if (project is null)
            return null;

        return new ProjectDto
        {
            Id = project.Id.ToString(),
            Title = project.Title,
            Description = project.Description
        };
    }

    public async Task<List<ProjectDto>> GetAllByUserAsync(Guid ownerId)
    {
        var projects =  await _projectRepository.GetByOwnerAsync(ownerId);
        
    }

    public async Task<Guid> CreateAsync(ProjectCreateDto dto, Guid ownerId)
    {
        var project = new Project(dto.Title.Trim(), dto.Description, ownerId);

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return project.Id;
    }

    public async Task<bool> UpdateAsync(Guid projectId, ProjectUpdateDto dto, Guid ownerId)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId && p.OwnerId == ownerId);

        if (project is null)
            return false;

        project.UpdateDetails(dto.Title.Trim(), dto.Description);

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, Guid ownerId)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id && p.OwnerId == ownerId);

        if (project is null)
            return false;

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        return true;
    }
}
