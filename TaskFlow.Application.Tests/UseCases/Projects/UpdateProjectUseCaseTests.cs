using FluentAssertions;
using Moq;
using TaskFlow.Application.Abstractions;
using TaskFlow.Application.Interfaces;
using TaskFlow.Application.UseCases.Projects.Update;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Exceptions;
using TaskFlow.Core.Factories;

namespace TaskFlow.Application.Tests.UseCases.Projects;

public sealed class UpdateProjectUseCaseTests
{
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    private readonly UpdateProjectUseCase _useCase;

    public UpdateProjectUseCaseTests()
    {
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _useCase = new UpdateProjectUseCase(
            _projectRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task HandleAsync_WhenProjectExists_ShouldUpdateAndCommit()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var project = ProjectFactory.Create(
            "Old title",
            "Old description",
            Guid.NewGuid());

        _projectRepositoryMock
            .Setup(r => r.GetByIdAsync(projectId))
            .ReturnsAsync(project);

        var command = new UpdateProjectCommand(
            projectId,
            "New title",
            "New description");

        // Act
        await _useCase.HandleAsync(command);

        // Assert
        project.Title.Value.Should().Be("New title");
        project.Description.Should().Be("New description");

        _unitOfWorkMock.Verify(
            u => u.CommitAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }
    [Fact]
    public async Task HandleAsync_WhenProjectDoesNotExist_ShouldThrowNotFoundException()
    {
        // Arrange
        var projectId = Guid.NewGuid();

        _projectRepositoryMock
            .Setup(r => r.GetByIdAsync(projectId))
            .ReturnsAsync((Project?)null);

        var command = new UpdateProjectCommand(
            projectId,
            "Title",
            "Description");

        // Act
        var act = async () => await _useCase.HandleAsync(command);

        // Assert
        await act.Should()
            .ThrowAsync<NotFoundException>();

        _unitOfWorkMock.Verify(
            u => u.CommitAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }
    [Fact]
    public async Task HandleAsync_WhenTitleIsInvalid_ShouldThrowDomainException()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var project = ProjectFactory.Create(
            "Valid title",
            null,
            Guid.NewGuid());

        _projectRepositoryMock
            .Setup(r => r.GetByIdAsync(projectId))
            .ReturnsAsync(project);

        var command = new UpdateProjectCommand(
            projectId,
            "",   // invalid title
            null);

        // Act
        var act = async () => await _useCase.HandleAsync(command);

        // Assert
        await act.Should()
            .ThrowAsync<DomainException>();

        _unitOfWorkMock.Verify(
            u => u.CommitAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }

}
