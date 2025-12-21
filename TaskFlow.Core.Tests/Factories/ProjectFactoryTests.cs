using TaskFlow.Core.Exceptions;
using TaskFlow.Core.Factories;

namespace TaskFlow.Core.Tests.Factories;

public class ProjectFactoryTests
{
    [Fact]
    public void Create_WithValidData_ShouldCreateProject()
    {
        // Act
        var project = ProjectFactory.Create(
            "TaskFlow",
            "Clean architecture project",
            Guid.NewGuid());

        // Assert
        Assert.NotNull(project);
        Assert.Equal("TaskFlow", project.Title.Value);
        Assert.NotEqual(Guid.Empty, project.Id);
    }

    [Fact]
    public void Create_WithEmptyTitle_ShouldThrowDomainException()
    {
        Assert.Throws<DomainException>(() =>
            ProjectFactory.Create(
                "",
                "Invalid project",
                Guid.NewGuid()));
    }

    [Fact]
    public void Create_ShouldTrimTitle()
    {
        var project = ProjectFactory.Create(
            "   Domain Driven Design   ",
            null,
            Guid.NewGuid());

        Assert.Equal("Domain Driven Design", project.Title.Value);
    }
}
