using Microsoft.Extensions.Caching.Memory;
using TaskFlow.Application.Interfaces;
using TaskFlow.Core.Repositories;

namespace TaskFlow.Infrastructure.Caching;

public class ProjectTitleCache(IMemoryCache cache, IProjectRepository projectRepo) : IProjectTitleCache
{
    private readonly IMemoryCache _cache = cache;
    private readonly IProjectRepository _projectRepo = projectRepo;

    public async Task<string> GetTitleAsync(Guid? id)
    {
        return await _cache.GetOrCreateAsync($"project-title-{id}", async e =>
        {
            e.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
            var p = await _projectRepo.GetProjectByIdAsync(id);
            return p?.Title ?? "";
        });
    }
}
