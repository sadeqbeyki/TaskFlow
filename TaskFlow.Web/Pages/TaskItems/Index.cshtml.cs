using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskFlow.Application.DTOs.TaskItems;
using TaskFlow.Application.Filters;
using TaskFlow.Application.Interfaces;

namespace TaskFlow.Web.Pages.TaskItems;

public class IndexModel : PageModel
{
    private readonly ITaskItemService _taskItemService;
    private readonly IProjectService _projectService;

    public IReadOnlyList<TaskItemDto> TaskItems { get; private set; } = [];

    public string ProjectTitle { get; private set; } = string.Empty;


    public IndexModel(ITaskItemService taskService, IProjectService projectService)
    {
        _taskItemService = taskService;
        _projectService = projectService;
    }

    //public async Task<IActionResult> OnGetAsync(Guid projectId)
    //{
    //    var ownerId = Guid.Parse("11111111-1111-1111-1111-111111111111");

    //    Tasks = await _taskService.GetAllByProjectAsync(projectId, ownerId);
    //    if (Tasks == null) 
    //        return NotFound();
    //    return Page();
    //}

    public async Task OnGetAsync([FromQuery] TaskItemFilter filter)
    {
        TaskItems = await _taskItemService.GetFilteredAsync(filter);
    }
}
