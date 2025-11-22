using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        var ownerId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        Tasks = await _taskService.GetAllByProjectAsync(projectId, ownerId);
        if (Tasks == null) 
            return NotFound();
        return Page();
    }

}
