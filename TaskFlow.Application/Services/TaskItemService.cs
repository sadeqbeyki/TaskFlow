using TaskFlow.Application.DTOs.TaskItems;
using TaskFlow.Application.Interfaces;
using TaskFlow.Application.Mappers;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Repositories;

namespace TaskFlow.Application.Services;

public class TaskItemService : ITaskItemService
{
    private readonly IGenericRepository<TaskItem, Guid> _genericRepository;
    private readonly ITaskItemRepository _taskItemRepository;

    public TaskItemService(IGenericRepository<TaskItem, Guid> genericRepository, ITaskItemRepository taskItemRepository)
    {
        _genericRepository = genericRepository;
        _taskItemRepository = taskItemRepository;
    }

    public async Task<TaskItemDto?> GetByIdAsync(Guid id, Guid ownerId)
    {
        // Ensure the task exists and belongs to a project owned by ownerId (authorization)
        var task = await _genericRepository.GetByIdAsync(id);

        if (task == null) return null;
        if (task.Project != null && task.Project.OwnerId != ownerId) return null; // not authorized

        return TaskItemMapper.MapToDto(task);
    }

    public async Task<List<TaskItemDto>> GetAllByProjectAsync(Guid projectId, Guid ownerId)
    {
        var taskList = await _taskItemRepository.GetByProjectAsync(projectId);

        if (taskList == null) return new List<TaskItemDto>();

        return taskList
            .Select(TaskItemMapper.MapToDto)
            .ToList();
    }

    public async Task<Guid> CreateAsync(TaskItemCreateDto dto, Guid ownerId)
    {
        //// Validate project exists and belongs to owner
        //var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == dto.ProjectId && p.OwnerId == ownerId)
        //    ?? throw new InvalidOperationException("Project not found or you do not have permission.");

        var task = new TaskItem(
            title: dto.Title.Trim(),
            description: dto.Description,
            projectId: dto.ProjectId,
            dueDate: dto.DueDate,
            priority: dto.Priority
        );

        await _genericRepository.AddAsync(task);
        return task.Id;
    }

    public async Task<bool> UpdateAsync(Guid id, TaskItemUpdateDto dto, Guid ownerId)
    {
        var task = await _genericRepository.GetByIdAsync(id);

        if (task == null) return false;
        if (task.Project != null && task.Project.OwnerId != ownerId) return false; // not authorized

        task.UpdateDetails(dto.Title.Trim(), dto.Description, dto.DueDate, dto.Priority);

        await _genericRepository.UpdateAsync(task);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, Guid ownerId)
    {
        var task = await _genericRepository.GetByIdAsync(id);

        if (task == null) return false;
        if (task.Project != null && task.Project.OwnerId != ownerId) return false; // not authorized

        _genericRepository.Remove(task);
        return true;
    }

    public async Task<bool> ChangeStatusAsync(Guid id, TaskItemStatusUpdateDto dto, Guid ownerId)
    {
        var task = await _taskItemRepository.ChangeStatusAsync(id, dto.Status, ownerId);

        if (task == true)
            return true;
        return false;
    }

    // ---------------------------------------------------------
    // Status changes
    // ---------------------------------------------------------
    public async Task<bool> MarkInProgressAsync(Guid id, Guid ownerId)
    {
        var task = await _taskItemRepository.MarkInProgressAsync(id, ownerId);

        if (task == true)
            return true;
        return false;
    }

    public async Task<bool> MarkDoneAsync(Guid id, Guid ownerId)
    {
        var task = await _taskItemRepository.MarkDoneAsync(id, ownerId);

        if (task == true)
            return true;
        return false;
    }

    public async Task<bool> ReopenAsync(Guid id, Guid ownerId)
    {
        var task = await _taskItemRepository.ReopenAsync(id, ownerId);

        if (task == true)
            return true;
        return false;
    }
}
