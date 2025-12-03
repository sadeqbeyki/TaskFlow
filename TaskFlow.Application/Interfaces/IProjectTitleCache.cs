namespace TaskFlow.Application.Interfaces;

public interface IProjectTitleCache
{
    Task<string> GetTitleAsync(Guid projectId);

}
