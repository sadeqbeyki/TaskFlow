using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskFlow.Application.DTOs.TaskItems;
using TaskFlow.Application.Interfaces;
using TaskFlow.Web.Common;
using TaskFlow.Web.Pages.TaskItems.Models;

namespace TaskFlow.Web.Pages.TaskItems;

public class CreateModel(ITaskItemService taskService, IProjectService projectService, IMapper mapper) : BasePageModel
{
    private readonly ITaskItemService _taskItemService = taskService;
    private readonly IProjectService _projectService = projectService;
    private readonly IMapper _mapper = mapper;

    [BindProperty]
    public TaskItemInputModel InputModel { get; set; } = new();
    public SelectList ProjectList { get; set; } = new SelectList(new List<SelectListItem>());


    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? projectId)
    {
        var projects = await _projectService.GetAllByUserAsync(OwnerId);
        ProjectList = new SelectList(projects, "Id", "Title", projectId?.ToString());

        if (projectId.HasValue)
            InputModel.ProjectId = projectId.Value;

        return Page();
    }


    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var taskItem = _mapper.Map<TaskItemCreateDto>(InputModel);

        try
        {
            var id = await _taskItemService.CreateAsync(taskItem, OwnerId);
            TempData["Message"] = "Task created successfully.";
            return RedirectToPage("Index");
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }
}
