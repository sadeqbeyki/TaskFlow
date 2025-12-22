using TaskFlow.Application.Interfaces;
using TaskFlow.Core.Entities;

namespace TaskFlow.Infrastructure.Repositories.Fakes;

public sealed class FakeProjectRepository : IProjectRepository
{
    public Project? AddedProject { get; private set; }

    public void Add(Project project)
    {
        AddedProject = project;
    }
}
