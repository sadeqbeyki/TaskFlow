using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskFlow.Application.Interfaces;
using TaskFlow.Web.Pages.TaskItems.Models;

namespace TaskFlow.Web.Pages.TaskItems;

public class DeleteModel : PageModel
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
    private readonly Guid _fakeOwnerId = Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee");

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        var dto = await _taskItemService.GetByIdAndOwnerAsync(id, _fakeOwnerId);
        if (dto == null)
            return NotFound();

        viewModel = _mapper.Map<TaskItemViewModel>(dto);
        return Page();
    }


    public async Task<IActionResult> OnPostAsync(Guid id)
    {
        try
        {
            await _taskItemService.DeleteAsync(id, _fakeOwnerId);
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
