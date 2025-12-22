using TaskFlow.Application.UseCases.Projects.Create;
using TaskFlow.Infrastructure.Repositories.Fakes;

namespace TaskFlow.Application.Tests.UseCases.Create;

public class CreateProjectUseCaseTests
{
    [Fact]
    public async Task HandleAsync_WithValidCommand_ShouldCreateProjectAndCommit()
    {
        var repository = new FakeProjectRepository();
        var unitOfWork = new FakeUnitOfWork();

        var useCase = new CreateProjectUseCase(repository, unitOfWork);

        var command = new CreateProjectCommand(
            title: "My Project",
            description: "Test project",
            ownerId: Guid.NewGuid()
        );

        var result = await useCase.HandleAsync(command);

        Assert.NotNull(repository.AddedProject);
        Assert.True(unitOfWork.Committed);
        Assert.Equal(repository.AddedProject!.Id, result.ProjectId);
    }
    [Fact]
    public async Task HandleAsync_WithInvalidTitle_ShouldThrowAndNotCommit()
    {
        var repository = new FakeProjectRepository();
        var unitOfWork = new FakeUnitOfWork();

        var useCase = new CreateProjectUseCase(repository, unitOfWork);

        var command = new CreateProjectCommand(
            title: "",
            description: null,
            ownerId: Guid.NewGuid()
        );

        await Assert.ThrowsAsync<Exception>(() =>
            useCase.HandleAsync(command));

        Assert.False(unitOfWork.Committed);
        Assert.Null(repository.AddedProject);
    }
}
