using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Interfaces;
using TaskFlow.Web.Common;
using TaskFlow.Web.Pages.TaskItems.Models;

namespace TaskFlow.Web.Pages.TaskItems;

public class DeleteModel : BasePageModel
{
    private readonly ITaskItemService _taskItemService;
    private readonly IMapper _mapper;

    public DeleteModel(ITaskItemService taskItemService, IMapper mapper)
    {
        _taskItemService = taskItemService;
        _mapper = mapper;
    }

    [BindProperty]
    public TaskItemViewModel viewModel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        var dto = await _taskItemService.GetByIdAndOwnerAsync(id, OwnerId);
        if (dto == null)
            return NotFound();

        viewModel = _mapper.Map<TaskItemViewModel>(dto);
        return Page();
    }


    public async Task<IActionResult> OnPostAsync(Guid id)
    {
        try
        {
            await _taskItemService.DeleteAsync(id, OwnerId);
            TempData["Message"] = "Task deleted successfully.";
            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"An error has occurred.: {ex.Message}");
            return Page();
        }
    }
}
