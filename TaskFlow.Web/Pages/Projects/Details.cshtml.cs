using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskFlow.Application.Interfaces;
using TaskFlow.Application.DTOs.Projects;

namespace TaskFlow.Web.Pages.Projects
{
    public class DetailsModel : PageModel
    {
        private readonly IProjectService _projectService;

       
        private readonly Guid _fakeOwnerId = Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee");

        public ProjectDto? Project { get; private set; }

        public DetailsModel(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Project = await _projectService.GetByIdAsync(id, _fakeOwnerId);

            if (Project == null)
                return NotFound();

            return Page();
        }
    }
}
