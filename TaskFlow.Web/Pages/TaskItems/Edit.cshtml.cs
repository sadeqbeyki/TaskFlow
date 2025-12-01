using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.DTOs.TaskItems;
using TaskFlow.Application.Interfaces;
using TaskFlow.Core.Entities;
using TaskFlow.Web.Pages.TaskItems.Models;

namespace TaskFlow.Web.Pages.TaskItems;

public class EditModel : PageModel
{
    private readonly ITaskItemService _taskItemService;
    private readonly IMapper _mapper;

    public EditModel(ITaskItemService taskItemService, IMapper mapper)
    {
        _taskItemService = taskItemService;
        _mapper = mapper;
    }


    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    private Guid OwnerId => Guid.Parse("11111111-1111-1111-1111-111111111111");

    [BindProperty]
    public TaskItemInputModel inputModel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        var taskItem = await _taskItemService.GetByIdAndOwnerAsync(Id, OwnerId);
        if (taskItem == null)
            return NotFound();

        inputModel = _mapper.Map<TaskItemInputModel>(taskItem);

        return Page();
    }


    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var dto = _mapper.Map<TaskItemUpdateDto>(inputModel);
        dto.Id = Id;
        var operation = await _taskItemService.UpdateAsync(Id, dto, OwnerId);
        if (!operation)
        {
            ModelState.AddModelError(string.Empty, "Unable to update task.");
            return Page();
        }

        TempData["Message"] = "Task updated successfully.";
        return RedirectToPage("Index");
    }
}
