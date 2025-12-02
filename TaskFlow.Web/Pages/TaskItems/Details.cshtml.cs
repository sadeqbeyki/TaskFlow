using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Interfaces;
using TaskFlow.Web.Common;
using TaskFlow.Web.Pages.TaskItems.Models;

namespace TaskFlow.Web.Pages.TaskItems;

public class DetailsModel(ITaskItemService taskItemService, IMapper mapper) : BasePageModel
{
    private readonly ITaskItemService _taskItemService = taskItemService;
    private readonly IMapper _mapper = mapper;

    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public TaskItemViewModel ViewModel { get; set; } = new();


    public async Task<IActionResult> OnGetAsync()
    {
        var dto = await _taskItemService.GetDetailsAsync(Id, OwnerId);
        if (dto == null)
            return NotFound();

        ViewModel = _mapper.Map<TaskItemViewModel>(dto);
        return Page();
    }
}
