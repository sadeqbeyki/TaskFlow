using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Core.Repositories;

namespace TaskFlow.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly TaskFlowDbContext _context;
    protected readonly DbSet<T> _db;

    public Repository(TaskFlowDbContext context)
    {
        _context = context;
        _db = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _db.FindAsync(id);
    }

    public IQueryable<T> Query()
    {
        return _db.AsQueryable();
    }

    public async Task AddAsync(T entity)
    {
        await _db.AddAsync(entity);
    }

    public void Remove(T entity)
    {
        _db.Remove(entity);
    }
}
