using TaskFlow.Application.DTOs.TaskItems;
using TaskFlow.Application.Filters;

namespace TaskFlow.Application.Interfaces;

public interface ITaskItemService
{
    Task<TaskItemDto?> GetByIdAndOwnerAsync(Guid id, Guid ownerId);
    Task<List<TaskItemDto>> GetAllByProjectAsync(Guid projectId, Guid ownerId);

    Task<Guid> CreateAsync(TaskItemCreateDto dto, Guid ownerId);
    Task<bool> UpdateAsync(Guid id, TaskItemUpdateDto dto, Guid ownerId);
    Task<bool> DeleteAsync(Guid id, Guid ownerId);

    Task<bool> ChangeStatusAsync(Guid id, TaskItemStatusUpdateDto dto, Guid ownerId);

    Task<bool> MarkInProgressAsync(Guid id, Guid ownerId);
    Task<bool> MarkDoneAsync(Guid id, Guid ownerId);
    Task<bool> ReopenAsync(Guid id, Guid ownerId);
    Task<IReadOnlyList<TaskItemDto>> GetFilteredAsync(TaskItemFilter filter);
}
