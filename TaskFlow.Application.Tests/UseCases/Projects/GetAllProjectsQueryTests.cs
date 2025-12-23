using FluentAssertions;
using Moq;
using TaskFlow.Application.Interfaces;
using TaskFlow.Application.UseCases.Projects.GetAll;
using TaskFlow.Core.Factories;

namespace TaskFlow.Application.Tests.UseCases.Projects;

public sealed class GetAllProjectsQueryTests
{
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly GetAllProjectsQueryHandler _query;

    public GetAllProjectsQueryTests()
    {
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _query = new GetAllProjectsQueryHandler(_projectRepositoryMock.Object);
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnAllProjects()
    {
        // Arrange
        var projects = new[]
        {
            ProjectFactory.Create("P1", null, Guid.NewGuid()),
            ProjectFactory.Create("P2", null, Guid.NewGuid())
        };

        _projectRepositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(projects);

        var query = new GetAllProjectsQuery();

        // Act
        var result = await _query.HandleAsync(query);

        // Assert
        result.Should().HaveCount(2);
        result.Select(p => p.Title)
              .Should().Contain(new[] { "P1", "P2" });
    }
}