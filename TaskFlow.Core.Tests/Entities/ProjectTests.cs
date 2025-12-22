using TaskFlow.Core.Entities;
using TaskFlow.Core.Exceptions;
using TaskFlow.Core.ValueObjects;

namespace TaskFlow.Core.Tests.Entities;
public class ProjectTests
{
    [Fact]
    public void AddTask_ShouldCreateTaskAndAttachToProject()
    {
        // Arrange
        var projectTitle = new ProjectTitle("My Project");
        var project = new Project(projectTitle, "desc", Guid.NewGuid());

        var taskTitle = new TaskTitle("My Task");

        // Act
        var task = project.AddTask(
            title: taskTitle,
            description: "task desc",
            dueDate: DateTime.UtcNow.AddDays(1)
        );

        // Assert
        Assert.NotNull(task);
        Assert.Single(project.Tasks);
        Assert.Equal(project.Id, task.ProjectId);
        Assert.Equal(taskTitle, task.Title);
    }

    [Fact]
    public void AddTask_WithPastDueDate_ShouldThrowDomainException()
    {
        // Arrange
        var project = new Project(
            new ProjectTitle("My Project"),
            null,
            Guid.NewGuid()
        );

        var taskTitle = new TaskTitle("Task");

        // Act
        var act = () =>
            project.AddTask(
                taskTitle,
                null,
                DateTime.UtcNow.AddDays(-1)
            );

        // Assert
        Assert.Throws<DomainException>(act);
    }

    [Fact]
    public void AddTask_WithInvalidTitle_ShouldFailBeforeFactory()
    {
        // Arrange
        var project = new Project(
            new ProjectTitle("Project"),
            null,
            Guid.NewGuid()
        );

        // Act
        var act = () =>
            project.AddTask(
                new TaskTitle("   "),
                null
            );

        // Assert
        Assert.Throws<DomainException>(act);
    }

    [Fact]
    public void Project_IsAggregateRoot_ForTaskCreation()
    {
        // Arrange
        var project = new Project(
            new ProjectTitle("Project"),
            null,
            Guid.NewGuid()
        );

        // Act
        var task = project.AddTask(
            new TaskTitle("Task"),
            null
        );

        // Assert
        Assert.Contains(task, project.Tasks);
    }
}

