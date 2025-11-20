using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskFlow.Application.DTOs.Projects;
using TaskFlow.Application.Interfaces;
using System.Security.Claims;

namespace TaskFlow.Web.Pages.Projects;

public class IndexModel : PageModel
{
    private readonly IProjectService _projectService;
    public List<ProjectDto> Projects { get; private set; } = new();

    public IndexModel(IProjectService projectService)
    {
        _projectService = projectService;
    }

    public async Task OnGetAsync()
    {
        var ownerId = GetCurrentUserId();
        Projects = await _projectService.GetAllByUserAsync(ownerId);
    }

    private Guid GetCurrentUserId()
    {
        var id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(id, out var guid))
        {
            // for dev: return a test guid or throw
            throw new InvalidOperationException("User not authenticated.");
        }
        return guid;
    }
}
