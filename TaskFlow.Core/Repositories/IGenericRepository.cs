using TaskFlow.Core.Specifications;

namespace TaskFlow.Core.Repositories;

public interface IGenericRepository<T, TKey> where T : class
{
    Task<T?> GetByIdAsync(TKey id);
    Task<List<T>> GetAllAsync();
    IQueryable<T> Query();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity); 
    void Remove(T entity);
    Task DeleteAsync(TKey id);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T>? spec = null);



}
