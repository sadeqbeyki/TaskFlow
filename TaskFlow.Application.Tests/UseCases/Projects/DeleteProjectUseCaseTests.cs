using FluentAssertions;
using Moq;
using TaskFlow.Application.Abstractions;
using TaskFlow.Application.Interfaces;
using TaskFlow.Application.UseCases.Projects.Delete;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Exceptions;
using TaskFlow.Core.Factories;

namespace TaskFlow.Application.Tests.UseCases.Projects;

public sealed class DeleteProjectUseCaseTests
{
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly DeleteProjectUseCase _useCase;

    public DeleteProjectUseCaseTests()
    {
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _useCase = new DeleteProjectUseCase(
            _projectRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task HandleAsync_WhenProjectExists_ShouldRemoveAndCommit()
    {
        // Arrange
        var project = ProjectFactory.Create("Test", null, Guid.NewGuid());

        _projectRepositoryMock
            .Setup(r => r.GetByIdAsync(project.Id))
            .ReturnsAsync(project);

        var command = new DeleteProjectCommand(project.Id);

        // Act
        await _useCase.HandleAsync(command);

        // Assert
        _projectRepositoryMock.Verify(
            r => r.Remove(project),
            Times.Once);

        _unitOfWorkMock.Verify(
            u => u.CommitAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task HandleAsync_WhenProjectDoesNotExist_ShouldThrowNotFound()
    {
        // Arrange
        var projectId = Guid.NewGuid();

        _projectRepositoryMock
            .Setup(r => r.GetByIdAsync(projectId))
            .ReturnsAsync((Project?)null);

        var command = new DeleteProjectCommand(projectId);

        // Act
        var act = async () => await _useCase.HandleAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();

        _unitOfWorkMock.Verify(
            u => u.CommitAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
