using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using TaskFlow.Application.DTOs.TaskItems;
using TaskFlow.Application.Interfaces;

namespace TaskFlow.Web.Pages.TaskItems;

public class IndexModel : PageModel
{
    private readonly ITaskItemService _taskService;
    private readonly IProjectService _projectService;

    public List<TaskItemDto> Tasks { get; private set; } = new();
    public string ProjectTitle { get; private set; } = string.Empty;

    public IndexModel(ITaskItemService taskService, IProjectService projectService)
    {
        _taskService = taskService;
        _projectService = projectService;
    }

    public async Task<IActionResult> OnGetAsync(Guid projectId)
    {
        var ownerId = GetCurrentUserId();

        // optional: verify project exists and belongs to user
        var projectDto = await _projectService.GetByIdAsync(projectId, ownerId);
        if (projectDto == null) return NotFound();

        ProjectTitle = projectDto.Title;
        Tasks = await _taskService.GetAllByProjectAsync(projectId, ownerId);
        return Page();
    }

    private Guid GetCurrentUserId()
    {
        var id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(id, out var guid))
            throw new InvalidOperationException("User not authenticated.");
        return guid;
    }
}
