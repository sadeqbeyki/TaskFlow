using Microsoft.Extensions.Caching.Memory;
using TaskFlow.Application.Interfaces;
using TaskFlow.Core.Repositories;

namespace TaskFlow.Infrastructure.Caching;

using Microsoft.Extensions.Caching.Memory;

public sealed class ProjectTitleCache : IProjectTitleCache
{
    private readonly IMemoryCache _cache;
    private readonly IProjectRepository _projectRepository;

    private readonly MemoryCacheEntryOptions _options;

    public ProjectTitleCache(IMemoryCache cache, IProjectRepository repo)
    {
        _cache = cache;
        _projectRepository = repo;

        _options = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))   // TTL  
            .SetSlidingExpiration(TimeSpan.FromMinutes(3));    // تمدید هوشمند  
    }

    public async Task<string?> GetTitleAsync(Guid projectId)
    {
        if (_cache.TryGetValue(projectId, out string? title))
            return title;

        var project = await _projectRepository.GetByIdAsync(projectId);
        if (project == null) return null;

        title = project.Title;
        _cache.Set(projectId, title, _options);
        return title;
    }

    public Task SetTitleAsync(Guid projectId, string title)
    {
        _cache.Set(projectId, title, _options);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(Guid projectId)
    {
        _cache.Remove(projectId);
        return Task.CompletedTask;
    }
}

