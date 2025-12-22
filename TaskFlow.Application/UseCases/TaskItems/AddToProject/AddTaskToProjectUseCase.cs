using TaskFlow.Application.Abstractions;
using TaskFlow.Application.Interfaces;
using TaskFlow.Core.Exceptions;
using TaskFlow.Core.ValueObjects;

namespace TaskFlow.Application.UseCases.TaskItems.AddToProject;

public interface IAddTaskToProjectUseCase
{
    Task<AddTaskToProjectResult> HandleAsync(AddTaskToProjectCommand command);
}

public sealed class AddTaskToProjectUseCaseHandler
    : IAddTaskToProjectUseCase
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddTaskToProjectUseCaseHandler(
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<AddTaskToProjectResult> HandleAsync(
        AddTaskToProjectCommand command)
    {
        var project = await _projectRepository.GetByIdAsync(command.ProjectId);

        if (project is null)
            throw new NotFoundException(
                $"Project with id '{command.ProjectId}' was not found.");

        var taskTitle = new TaskTitle(command.Title);

        var task = project.AddTask(
            title: taskTitle,
            description: command.Description,
            dueDate: command.DueDate,
            priority: command.Priority);

        await _unitOfWork.CommitAsync();

        return new AddTaskToProjectResult(task.Id);
    }
}