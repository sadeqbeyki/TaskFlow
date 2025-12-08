using System.Linq.Expressions;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Specifications;

namespace TaskFlow.Core.Repositories;

public interface ITaskItemRepository : IGenericRepository<TaskItem, Guid>
{
    Task<TaskItem?> GetWithProjectAsync(Guid id, Guid ownerId);

    Task<List<TaskItem>> GetByProjectAsync(Guid projectId);

    Task<List<TaskItem>> GetByStatusAsync(TaskItemStatus status);

    Task<bool> ValidateProjectOwnerAsync(Guid projectId, Guid ownerId);

    Task<int> CountAsync(ISpecification<TaskItem> spec, CancellationToken cancellationToken = default);
    Task<List<TResult>> ListAsync<TResult>(ISpecification<TaskItem> spec, Expression<Func<TaskItem, TResult>> selector, CancellationToken cancellationToken = default);
    Task SaveAsync();
}
