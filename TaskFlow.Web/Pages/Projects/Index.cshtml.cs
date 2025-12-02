using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.DTOs.Projects;
using TaskFlow.Application.Interfaces;
using TaskFlow.Web.Common;

namespace TaskFlow.Web.Pages.Projects;

public class IndexModel(IProjectService projectService) : BasePageModel
{
    private readonly IProjectService _projectService= projectService;

    [BindProperty(SupportsGet = true)]
    public List<ProjectDto> Projects { get; private set; } = new();
    public async Task OnGetAsync()
    {
        Projects = await _projectService.GetAllByUserAsync(OwnerId);
    }
}
