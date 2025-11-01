using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.DTOs.Projects;
using TaskFlow.Application.Interfaces;
using TaskFlow.Core;
using TaskFlow.Infrastructure;

namespace TaskFlow.Application.Services;


public class ProjectService : IProjectService
{
    private readonly TaskFlowDbContext _context;

    public ProjectService(TaskFlowDbContext context)
    {
        _context = context;
    }

    public async Task<ProjectDto?> GetByIdAsync(Guid id)
    {
        var project = await _context.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

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
        return await _context.Projects
            .AsNoTracking()
            .Where(p => p.OwnerId == ownerId)
            .Select(p => new ProjectDto
            {
                Id = p.Id.ToString(),
                Title = p.Title,
                Description = p.Description
            })
            .ToListAsync();
    }

    public async Task<Guid> CreateAsync(ProjectCreateDto dto, Guid ownerId)
    {
        var project = new Project(dto.Title.Trim(), dto.Description, ownerId);

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return project.Id;
    }

    public async Task<bool> UpdateAsync(Guid id, ProjectUpdateDto dto, Guid ownerId)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id && p.OwnerId == ownerId);

        if (project is null)
            return false;

        project.UpdateDetails(dto.Title, dto.Description);

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
