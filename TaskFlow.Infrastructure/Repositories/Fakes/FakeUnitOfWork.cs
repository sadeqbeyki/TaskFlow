using TaskFlow.Application.Abstractions;

namespace TaskFlow.Infrastructure.Repositories.Fakes
{
    public sealed class FakeUnitOfWork : IUnitOfWork
    {
        public bool Committed { get; private set; }

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            Committed = true;
            return Task.CompletedTask;
        }
    }
}
