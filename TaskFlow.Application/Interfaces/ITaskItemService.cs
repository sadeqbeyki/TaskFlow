using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.DTOs.TaskItems;

namespace TaskFlow.Application.Interfaces;

internal interface ITaskItemService
{
    Task<TaskItemDto?> GetByIdAsync(Guid id, Guid ownerId);
    Task<List<TaskItemDto>> GetAllByProjectAsync(Guid projectId, Guid ownerId);

    Task<Guid> CreateAsync(TaskItemCreateDto dto, Guid ownerId);
    Task<bool> UpdateAsync(Guid id, TaskItemUpdateDto dto, Guid ownerId);
    Task<bool> DeleteAsync(Guid id, Guid ownerId);

    Task<bool> ChangeStatusAsync(Guid id, TaskItemStatusUpdateDto dto, Guid ownerId);
}
