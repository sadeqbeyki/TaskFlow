using FluentAssertions;
using Moq;
using TaskFlow.Application.Interfaces;
using TaskFlow.Application.UseCases.Projects.GetAll;
using TaskFlow.Application.UseCases.Projects.GetById;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Exceptions;
using TaskFlow.Core.Factories;

namespace TaskFlow.Application.Tests.UseCases.Projects;

public sealed class GetProjectByIdQueryTests
{
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly GetProjectByIdQueryHandler _query;

    public GetProjectByIdQueryTests()
    {
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _query = new GetProjectByIdQueryHandler(_projectRepositoryMock.Object);
    }

    [Fact]
    public async Task HandleAsync_WhenProjectExists_ShouldReturnProject()
    {
        // Arrange
        var project = ProjectFactory.Create("Title", null, Guid.NewGuid());

        _projectRepositoryMock
            .Setup(r => r.GetByIdAsync(project.Id))
            .ReturnsAsync(project);

        var query = new GetProjectByIdQuery(project.Id);

        // Act
        var result = await _query.HandleAsync(query);

        // Assert
        result.Id.Should().Be(project.Id);
        result.Title.Should().Be("Title");
    }

    [Fact]
    public async Task HandleAsync_WhenProjectDoesNotExist_ShouldThrowNotFound()
    {
        // Arrange
        var projectId = Guid.NewGuid();

        _projectRepositoryMock
            .Setup(r => r.GetByIdAsync(projectId))
            .ReturnsAsync((Project?)null);

        var query = new GetProjectByIdQuery(projectId);
        // Act
        var act = async () => await _query.HandleAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}