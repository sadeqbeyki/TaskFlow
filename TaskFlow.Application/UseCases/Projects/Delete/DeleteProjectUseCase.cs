using TaskFlow.Application.Abstractions;
using TaskFlow.Application.Interfaces;
using TaskFlow.Core.Exceptions;

namespace TaskFlow.Application.UseCases.Projects.Delete;

public sealed class DeleteProjectUseCase
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProjectUseCase(
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<DeleteProjectResult> HandleAsync(
        DeleteProjectCommand command,
        CancellationToken cancellationToken = default)
    {
        var project = await _projectRepository.GetByIdAsync(
            command.ProjectId);

        if (project is null)
            throw new NotFoundException(
                $"Project with id '{command.ProjectId}' was not found.");

        _projectRepository.Remove(project);

        await _unitOfWork.CommitAsync(cancellationToken);

        return new DeleteProjectResult(project.Id);
    }
}