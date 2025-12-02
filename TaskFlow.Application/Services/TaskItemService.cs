using AutoMapper;
using System.Linq.Expressions;
using TaskFlow.Application.DTOs.TaskItems;
using TaskFlow.Application.Interfaces;
using TaskFlow.Application.Mappers;
using TaskFlow.Application.Specifications;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Filters;
using TaskFlow.Core.Repositories;

namespace TaskFlow.Application.Services;

public class TaskItemService : ITaskItemService
{
    private readonly IGenericRepository<TaskItem, Guid> _genericRepository;
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly IMapper _mapper;

    public TaskItemService(IGenericRepository<TaskItem, Guid> genericRepository, ITaskItemRepository taskItemRepository, IMapper mapper)
    {
        _genericRepository = genericRepository;
        _taskItemRepository = taskItemRepository;
        _mapper = mapper;
    }

    public async Task<TaskItemDto?> GetByIdAndOwnerAsync(Guid id, Guid ownerId)
    {
        var task = await _genericRepository.GetByIdAsync(id);

        return task is null ? null : TaskItemMapper.MapToDto(task);
    }

    public async Task<List<TaskItemDto>> GetAllByProjectAsync(Guid projectId, Guid ownerId)
    {
        var taskList = await _taskItemRepository.GetByProjectAsync(projectId);

        return taskList
            .Select(TaskItemMapper.MapToDto)
            .ToList();
    }

    public async Task<Guid> CreateAsync(TaskItemCreateDto dto, Guid ownerId)
    {
        var projectExists = await _taskItemRepository.ValidateProjectOwnerAsync(dto.ProjectId, ownerId);

        if (!projectExists)
            throw new UnauthorizedAccessException("Project does not belong to this user.");


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

        if (task == null)
            return false;

        task.UpdateDetails(dto.Title.Trim(), dto.Description, dto.DueDate, dto.Priority, dto.ProjectId);

        await _genericRepository.UpdateAsync(task);

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, Guid ownerId)
    {
        var task = await _genericRepository.GetByIdAsync(id);

        if (task == null) return false;
        await _genericRepository.DeleteAsync(id);
        return true;
    }


    // Begin-Filters
    public async Task<(IReadOnlyList<TaskItemDto> Items, int TotalCount)> GetFilteredItemsAsync(TaskItemFilter filter)
    {
        var spec = new TaskItemSpecification(filter);

        var selector = (Expression<Func<TaskItem, TaskItemDto>>)(t => new TaskItemDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            DueDate = t.DueDate,
            Priority = t.Priority,
            Status = t.Status,
            ProjectId = t.ProjectId,
            CreatedAt = t.CreatedAt,
            UpdatedAt = t.UpdatedAt
        });

        var dtos = await _taskItemRepository.ListAsync(spec, selector);
        var totalCount = await _taskItemRepository.CountAsync(spec);

        return (dtos, totalCount);
    }
    // End-Filters

    // ---------------------------------------------------------
    // Status changes
    // ---------------------------------------------------------
    public async Task<bool> ChangeStatusAsync(Guid id, TaskItemStatusUpdateDto dto, Guid ownerId)
        => await _taskItemRepository.ChangeStatusAsync(id, dto.Status, ownerId);

    public async Task<bool> MarkInProgressAsync(Guid id, Guid ownerId)
        => await _taskItemRepository.MarkInProgressAsync(id, ownerId);

    public async Task<bool> MarkDoneAsync(Guid id, Guid ownerId)
        => await _taskItemRepository.MarkDoneAsync(id, ownerId);


    public async Task<bool> ReopenAsync(Guid id, Guid ownerId)
        => await _taskItemRepository.ReopenAsync(id, ownerId);
}
