using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.DTOs.TaskItems;
using TaskFlow.Application.Interfaces;
using TaskFlow.Web.Common;
using TaskFlow.Web.Pages.TaskItems.Models;

namespace TaskFlow.Web.Pages.TaskItems;

public class EditModel(ITaskItemService taskItemService, IMapper mapper) : BasePageModel
{
    private readonly ITaskItemService _taskItemService = taskItemService;
    private readonly IMapper _mapper = mapper;

    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public TaskItemInputModel InputModel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        var taskItem = await _taskItemService.GetByIdAndOwnerAsync(Id, OwnerId);
        if (taskItem == null)
            return NotFound();

        InputModel = _mapper.Map<TaskItemInputModel>(taskItem);
        return Page();
    }


    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var dto = _mapper.Map<TaskItemUpdateDto>(InputModel);

        var operation = await _taskItemService.UpdateAsync(Id, dto, OwnerId);
        if (!operation)
        {
            SetError("Unable to update task.");
            return Page();
        }
        SetSuccess("Task updated successfully.");
        return RedirectToPage("Index");
    }
}
