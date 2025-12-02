using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Interfaces;
using TaskFlow.Application.DTOs.Projects;
using TaskFlow.Web.Common;

namespace TaskFlow.Web.Pages.Projects
{
    public class DetailsModel(IProjectService projectService) : BasePageModel
    {
        private readonly IProjectService _projectService = projectService;

        public ProjectDto? Project { get; private set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Project = await _projectService.GetByIdAsync(id, OwnerId);

            if (Project == null)
                return NotFound();

            return Page();
        }
    }
}
