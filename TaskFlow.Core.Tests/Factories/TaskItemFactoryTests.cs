using TaskFlow.Core.Entities;
using TaskFlow.Core.Exceptions;
using TaskFlow.Core.ValueObjects;
using TaskFlow.Core.Factories;

namespace TaskFlow.Core.Tests.Factories;

public class TaskItemFactoryTests
{
    [Fact]
    public void Create_WithValidData_ShouldCreateTask()
    {
        var title = new TaskTitle("Write tests");
        var projectId = Guid.NewGuid();

        var task = TaskItemFactory.Create(
            title,
            "Testing factory",
            projectId,
            DateTime.UtcNow.AddDays(1),
            TaskItemPriority.High);

        Assert.NotNull(task);
        Assert.Equal(title, task.Title);
        Assert.Equal(projectId, task.ProjectId);
        Assert.Equal(TaskItemPriority.High, task.Priority);
    }

    [Fact]
    public void Create_WithPastDueDate_ShouldThrowDomainException()
    {
        var title = new TaskTitle("Invalid task");

        Assert.Throws<DomainException>(() =>
            TaskItemFactory.Create(
                title,
                null,
                Guid.NewGuid(),
                DateTime.UtcNow.AddDays(-1),
                TaskItemPriority.Medium));
    }
}
