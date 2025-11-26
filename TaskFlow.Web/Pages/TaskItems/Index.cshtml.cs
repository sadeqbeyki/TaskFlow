using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskFlow.Application.DTOs.TaskItems;
using TaskFlow.Application.Interfaces;
using TaskFlow.Core.Filters;

namespace TaskFlow.Web.Pages.TaskItems;

public class IndexModel(ITaskItemService taskService) : PageModel
{
    private readonly ITaskItemService _taskItemService = taskService;

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
    }

}
