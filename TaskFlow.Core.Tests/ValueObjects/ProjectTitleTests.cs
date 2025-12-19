using TaskFlow.Core.ValueObjects;

namespace TaskFlow.Core.Tests.ValueObjects;

public class ProjectTitleTests
{
    [Fact]
    public void Create_WithValidTitle_ShouldSucceed()
    {
        // Act
        var title = new ProjectTitle("My Project");

        // Assert
        Assert.Equal("My Project", title.Value);
    }

    [Fact]
    public void Create_WithWhitespace_ShouldThrowException()
    {
        Assert.Throws<ArgumentException>(() =>
            new ProjectTitle("   "));
    }

    [Fact]
    public void Create_WithEmptyString_ShouldThrowException()
    {
        Assert.Throws<ArgumentException>(() =>
            new ProjectTitle(string.Empty));
    }

    [Fact]
    public void Create_ShouldTrimValue()
    {
        var title = new ProjectTitle("  Clean Architecture  ");

        Assert.Equal("Clean Architecture", title.Value);
    }
}