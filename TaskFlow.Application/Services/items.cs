using TaskFlow.Application.DTOs.TaskItems;
using TaskFlow.Core.Filters;

namespace TaskFlow.Application.Services
{
    public interface items
    {
        Task<bool> ChangeStatusAsync(Guid id, TaskItemStatusUpdateDto dto, Guid ownerId);
        Task<bool> CreateAsync(TaskItemCreateDto dto, Guid ownerId);
        Task<bool> DeleteAsync(Guid id, Guid ownerId);
        Task<List<TaskItemDto>> GetAllByProjectAsync(Guid projectId, Guid ownerId);
        Task<TaskItemViewDto?> GetDetailsAsync(Guid id, Guid ownerId);
        Task<(IReadOnlyList<TaskItemDto> Items, int TotalCount)> GetFilteredItemsAsync(TaskItemFilter filter);
        Task<bool> MarkDoneAsync(Guid id, Guid ownerId);
        Task<bool> MarkInProgressAsync(Guid id, Guid ownerId);
        Task<bool> ReopenAsync(Guid id, Guid ownerId);
        Task<bool> UpdateAsync(Guid id, TaskItemUpdateDto dto, Guid ownerId);
    }
}