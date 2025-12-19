using TaskFlow.Core.Entities;
using TaskFlow.Core.ValueObjects;

namespace TaskFlow.Core.Factories;

public static class ProjectFactory
{
    public static Project Create(
        string title,
        string? description,
        Guid ownerId)
    {
        var projectTitle = new ProjectTitle(title);

        return new Project(
            projectTitle,
            description,
            ownerId);
    }
}
