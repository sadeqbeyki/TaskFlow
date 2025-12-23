using TaskFlow.Application.Abstractions;
using TaskFlow.Application.Interfaces;
using TaskFlow.Core.Exceptions;
using TaskFlow.Core.ValueObjects;

namespace TaskFlow.Application.UseCases.Projects.Update;

public sealed class UpdateProjectUseCase
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProjectUseCase(
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateProjectResult> HandleAsync(
        UpdateProjectCommand command,
        CancellationToken cancellationToken = default)
    {
        var project = await _projectRepository
            .GetByIdAsync(command.ProjectId,cancellationToken);

        if (project is null)
            throw new NotFoundException(
                $"Project with id '{command.ProjectId}' was not found.");

        var title = new ProjectTitle(command.Title);

        project.UpdateDetails(
            title,
            command.Description);

        await _unitOfWork.CommitAsync(cancellationToken);

        return new UpdateProjectResult(project.Id);
    }
}