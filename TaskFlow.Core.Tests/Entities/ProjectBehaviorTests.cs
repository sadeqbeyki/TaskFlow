using TaskFlow.Core.Factories;
using TaskFlow.Core.ValueObjects;

namespace TaskFlow.Core.Tests.Entities;

public class ProjectBehaviorTests
{
    [Fact]
    public void AddTask_ShouldAddTaskToProject()
    {
        var project = ProjectFactory.Create(
            "Test Project",
            null,
            Guid.NewGuid());

        var taskTitle = new TaskTitle("Implement Aggregate");

        var task = project.AddTask(
            taskTitle,
            "Aggregate behavior test",
            DateTime.UtcNow.AddDays(2));

        Assert.Single(project.Tasks);
        Assert.Equal(task, project.Tasks.First());
    }

    [Fact]
    public void AddTask_ShouldUpdateProjectUpdatedAt()
    {
        var project = ProjectFactory.Create(
            "Time Test",
            null,
            Guid.NewGuid());

        var before = project.UpdatedAt;

        project.AddTask(
            new TaskTitle("Time sensitive task"),
            null,
            DateTime.UtcNow.AddDays(1));

        Assert.NotEqual(before, project.UpdatedAt);
    }
}
