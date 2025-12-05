namespace TaskFlow.Application.Interfaces;

public interface IProjectTitleCache
{
    Task<string?> GetTitleAsync(Guid projectId);
    Task SetTitleAsync(Guid projectId, string title);
    Task RemoveAsync(Guid projectId);
}


