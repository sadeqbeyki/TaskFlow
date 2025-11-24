using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using TaskFlow.Application.DTOs.TaskItems;
using TaskFlow.Application.Filters;
using TaskFlow.Application.Interfaces;
using TaskFlow.Application.Services;
using TaskFlow.Core.Entities;

namespace TaskFlow.Web.Pages.TaskItems;

public class IndexModel : PageModel
{
    private readonly ITaskItemService _taskItemService;
    private readonly IProjectService _projectService;

    public List<TaskItemDto> TaskItems { get; private set; } = new();

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
    public async Task OnGetAsync()
    {
        var filter = new TaskItemFilter
        {
            SearchText = Search,
            Status = Status,
            Priority = Priority,
            ProjectId = ProjectId
        };

        TaskItems = await _taskItemService.GetFilteredAsync(filter);
    }

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    [BindProperty(SupportsGet = true)]
    public TaskItemStatus? Status { get; set; }

    [BindProperty(SupportsGet = true)]
    public TaskItemPriority? Priority { get; set; }

    [BindProperty(SupportsGet = true)]
    public Guid? ProjectId { get; set; }
}
