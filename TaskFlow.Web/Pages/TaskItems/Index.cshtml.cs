using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.DTOs.TaskItems;
using TaskFlow.Application.Interfaces;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Filters;
using TaskFlow.Web.Common;
using TaskFlow.Web.Pages.TaskItems.Models;

namespace TaskFlow.Web.Pages.TaskItems;

public class IndexModel(ITaskItemService taskService, IProjectSummaryService projectSummaryService) : BasePageModel
{
    private readonly ITaskItemService _taskItemService = taskService;
    private readonly IProjectSummaryService _projectSummaryService = projectSummaryService;

    public IReadOnlyList<ProjectSummaryDto> Projects { get; set; } = [];
    public IReadOnlyList<TaskItemDto> TaskItems { get; private set; } = Array.Empty<TaskItemDto>();
    public int TotalCount { get; private set; }

    [BindProperty(SupportsGet = true)]
    public TaskItemFilter Filter { get; set; } = new();
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / Filter.PageSize);

    public async Task OnGetAsync()
    {
        var result = await _taskItemService.GetFilteredItemsAsync(Filter);
        TaskItems = result.Items;
        TotalCount = result.TotalCount;
        Projects = await _projectSummaryService.GetAllAsync(OwnerId);
    }
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostChangeStatusAsync([FromForm] Guid id, [FromForm] TaskItemStatusUpdateDto status)
    {
        var success = await _taskItemService.ChangeStatusAsync(id, status, OwnerId);

        if (!success)
        {
            Console.WriteLine($"ChangeStatus failed: id={id}, status={status}, owner={OwnerId}");
        }

        return new JsonResult(new { ok = true });
    }

}
