using TaskFlow.Application.Interfaces;
using TaskFlow.Core.Exceptions;

namespace TaskFlow.Application.UseCases.Projects.GetById;

public sealed class GetProjectByIdQueryHandler
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectByIdQueryHandler(
        IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ProjectDetailsDto> HandleAsync(
        GetProjectByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var project = await _projectRepository
            .GetByIdAsync(query.ProjectId);

        if (project is null)
            throw new NotFoundException(
                $"Project with id '{query.ProjectId}' was not found.");

        return new ProjectDetailsDto(
            project.Id,
            project.Title.Value,
            project.Description,
            project.CreatedAt
        );
    }
}