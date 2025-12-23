using TaskFlow.Application.Interfaces;

namespace TaskFlow.Application.UseCases.Projects.GetAll;

public sealed class GetAllProjectsQueryHandler
{
    private readonly IProjectRepository _projectRepository;

    public GetAllProjectsQueryHandler(
        IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<IReadOnlyList<ProjectSummaryDto>> HandleAsync(
        GetAllProjectsQuery query,
        CancellationToken cancellationToken = default)
    {
        var projects = await _projectRepository.GetAllAsync();

        return projects
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => new ProjectSummaryDto(
                p.Id,
                p.Title.Value,
                p.CreatedAt))
            .ToList();
    }
}