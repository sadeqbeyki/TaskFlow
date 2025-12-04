using AutoMapper;
using System.Linq.Expressions;
using TaskFlow.Application.DTOs.TaskItems;
using TaskFlow.Application.Interfaces;
using TaskFlow.Application.Mappers;
using TaskFlow.Application.Specifications;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Factories;
using TaskFlow.Core.Filters;
using TaskFlow.Core.Repositories;

namespace TaskFlow.Application.Services;

public class TaskItemService : ITaskItemService
{
    private readonly IGenericRepository<TaskItem, Guid> _genericRepository;
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly IMapper _mapper;
    private readonly IProjectTitleCache _projectTitleCache;

    public TaskItemService(
        IGenericRepository<TaskItem, Guid> genericRepository,
        ITaskItemRepository taskItemRepository,
        IMapper mapper,
        IProjectTitleCache projectTitleCache
        )
    {
        _genericRepository = genericRepository;
        _taskItemRepository = taskItemRepository;
        _mapper = mapper;
        _projectTitleCache = projectTitleCache;
    }

    public async Task<TaskItemViewDto?> GetDetailsAsync(Guid id, Guid ownerId)
    {
        var task = await _taskItemRepository.GetByIdWithProjectAsync(id, ownerId);
        return task == null 
            ? null 
            : _mapper.Map<TaskItemViewDto>(task);
    }

    public async Task<List<TaskItemDto>> GetAllByProjectAsync(Guid projectId, Guid ownerId)
    {
        var taskList = await _taskItemRepository.GetByProjectAsync(projectId);

        return _mapper.Map<List<TaskItemDto>>(taskList);
    }

    public async Task<bool> CreateAsync(TaskItemCreateDto dto, Guid ownerId)
    {
        var projectExists = await _taskItemRepository.ValidateProjectOwnerAsync(dto.ProjectId, ownerId);

        if (!projectExists)
            throw new UnauthorizedAccessException("Project does not belong to this user.");

        var task = TaskItemFactory.Create(
            dto.Title,
            dto.Description,
            dto.ProjectId,
            dto.DueDate,
            dto.Priority
        );

        await _genericRepository.AddAsync(task);

        return true;
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

    // ---------------------------------------------------------
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
            ProjectTitle = t.Project != null ? t.Project.Title : null,
            CreatedAt = t.CreatedAt,
            UpdatedAt = t.UpdatedAt
        });

        var dtos = await _taskItemRepository.ListAsync(spec, selector);
        var totalCount = await _taskItemRepository.CountAsync(spec);

        return (dtos, totalCount);
    }
    // End-Filters
    // ---------------------------------------------------------


    // ---------------------------------------------------------
    // Begin Status changes
    public async Task<bool> ChangeStatusAsync(Guid id, TaskItemStatusUpdateDto dto, Guid ownerId)
        => await _taskItemRepository.ChangeStatusAsync(id, dto.Status, ownerId);

    public async Task<bool> MarkInProgressAsync(Guid id, Guid ownerId)
        => await _taskItemRepository.MarkInProgressAsync(id, ownerId);

    public async Task<bool> MarkDoneAsync(Guid id, Guid ownerId)
        => await _taskItemRepository.MarkDoneAsync(id, ownerId);


    public async Task<bool> ReopenAsync(Guid id, Guid ownerId)
        => await _taskItemRepository.ReopenAsync(id, ownerId);
    // End Status changes
    // ---------------------------------------------------------
}
