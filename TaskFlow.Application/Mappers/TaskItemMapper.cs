using AutoMapper;
using TaskFlow.Application.DTOs.TaskItems;
using TaskFlow.Application.Interfaces;
using TaskFlow.Core.Entities;

namespace TaskFlow.Application.Mappers;

public class TaskItemMapper
{
    private readonly IMapper _mapper;
    private readonly IProjectTitleCache _projectTitleCache;

    public TaskItemMapper(IMapper mapper, IProjectTitleCache projectTitleCache)
    {
        _mapper = mapper;
        _projectTitleCache = projectTitleCache;
    }

    public async Task<TaskItemDto> MapAsync(TaskItem task)
    {
        var dto = _mapper.Map<TaskItemDto>(task);

        if (task.ProjectId != Guid.Empty)
        {
            dto.ProjectTitle = await _projectTitleCache.GetTitleAsync(task.ProjectId);
        }

        return dto;
    }
}
