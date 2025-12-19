using TaskFlow.Core.Exceptions;
using TaskFlow.Core.ValueObjects;

namespace TaskFlow.Core.Tests.ValueObjects;

public class TaskTitleTests
{
    [Fact]
    public void Create_WithValidTitle_ShouldCreateSuccessfully()
    {
        // Arrange
        var input = "  My Task Title  ";

        // Act
        var title = new TaskTitle(input);

        // Assert
        Assert.Equal("My Task Title", title.Value);
    }

    [Fact]
    public void Create_WithNull_ShouldThrowDomainException()
    {
        // Act & Assert
        Assert.Throws<DomainException>(() => new TaskTitle(null!));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithEmptyOrWhitespace_ShouldThrowDomainException(string input)
    {
        Assert.Throws<DomainException>(() => new TaskTitle(input));
    }

    [Fact]
    public void Create_WithTooLongValue_ShouldThrowDomainException()
    {
        // Arrange
        var longTitle = new string('a', 251);

        // Act & Assert
        Assert.Throws<DomainException>(() => new TaskTitle(longTitle));
    }

    [Fact]
    public void Two_TaskTitles_WithSameValue_ShouldBeEqual()
    {
        // Arrange
        var t1 = new TaskTitle("Test");
        var t2 = new TaskTitle("Test");

        // Assert
        Assert.Equal(t1, t2);
        Assert.True(t1 == t2);
        Assert.False(t1 != t2);
    }
}
