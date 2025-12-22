using TaskFlow.Application.Abstractions;

namespace TaskFlow.Infrastructure.Persistence;

public sealed class EfUnitOfWork : IUnitOfWork
{
    private readonly TaskFlowDbContext _context;

    public EfUnitOfWork(TaskFlowDbContext context)
    {
        _context = context;
    }

    public Task CommitAsync(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(cancellationToken);
}
