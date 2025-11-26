using Microsoft.EntityFrameworkCore;
using TaskFlow.Core.Repositories;
using TaskFlow.Core.Specifications;

namespace TaskFlow.Infrastructure.Repositories;

public class GenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : class
{
    protected readonly TaskFlowDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(TaskFlowDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(TKey id)
    {
        return await _dbSet.FindAsync(id); // FindAsync = Tracking ON
    }
    public async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public IQueryable<T> Query()
    {
        return _dbSet.AsQueryable();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity); // Tracking entity OK
        await _context.SaveChangesAsync();
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
        _context.SaveChanges();
    }

    public async Task DeleteAsync(TKey id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
            return;

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

}
