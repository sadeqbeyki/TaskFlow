using TaskFlow.Application.DTOs.TaskItems;

namespace TaskFlow.Application.Interfaces;

public interface IProjectSummaryService
{
    Task<List<ProjectSummaryDto>> GetAllAsync(Guid ownerId);
}
