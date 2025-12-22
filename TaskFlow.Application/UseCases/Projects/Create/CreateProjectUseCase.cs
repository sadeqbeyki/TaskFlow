
using TaskFlow.Application.Interfaces;
using TaskFlow.Core.Factories;

namespace TaskFlow.Application.UseCases.Projects.Create
{
    public sealed class CreateProjectUseCase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProjectUseCase(
            IProjectRepository projectRepository,
            IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateProjectResult> HandleAsync(
            CreateProjectCommand command,
            CancellationToken cancellationToken = default)
        {
            var project = ProjectFactory.Create(
                command.Title,
                command.Description,
                command.OwnerId);

            _projectRepository.Add(project);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new CreateProjectResult
            {
                ProjectId = project.Id
            };
        }
    }
}
