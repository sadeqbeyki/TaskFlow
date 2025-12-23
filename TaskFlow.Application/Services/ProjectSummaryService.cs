using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.DTOs.TaskItems;
using TaskFlow.Application.Interfaces;
using TaskFlow.Core.Repositories;

namespace TaskFlow.Application.Services;

public class ProjectSummaryService : IProjectSummaryService
{
    private readonly Core.Repositories.IProjectRepository _repo;

    public ProjectSummaryService(Core.Repositories.IProjectRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<ProjectSummaryDto>> GetAllAsync(Guid ownerId)
    {
        return await _repo.Query()
            .Where(p => p.OwnerId == ownerId)
            .Select(p => new ProjectSummaryDto
            {
                Id = p.Id,
                Name = p.Title,
            })
            .ToListAsync();
    }
}
