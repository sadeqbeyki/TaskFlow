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

    [BindProperty]
    public TaskItemInputModel inputModel { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    private Guid OwnerId => Guid.Parse("11111111-1111-1111-1111-111111111111"); 


    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        var taskItem = await _taskItemService.GetByIdAndOwnerAsync(id, OwnerId);
        if (taskItem == null)
            return NotFound();

        inputModel = _mapper.Map<TaskItemInputModel>(taskItem);
        Id = taskItem.Id;

        return Page();
    }


    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) 
            return Page();

        var taskItem = _mapper.Map<TaskItemUpdateDto>(inputModel);
        taskItem.Id = Id;
        var operation = await _taskItemService.UpdateAsync(Id, taskItem, OwnerId);
        if (!operation)
        {
            ModelState.AddModelError(string.Empty, "Unable to update task.");
            return Page();
        }

        TempData["Message"] = "Task updated successfully.";
        return RedirectToPage("Index");
    }
}
