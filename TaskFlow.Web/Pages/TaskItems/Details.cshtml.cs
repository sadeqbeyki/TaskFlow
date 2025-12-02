using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Services;
using TaskFlow.Web.Common;
using TaskFlow.Web.Pages.TaskItems.Models;

namespace TaskFlow.Web.Pages.TaskItems;

public class DetailsModel(TaskItemService taskItemService, IMapper mapper) : BasePageModel
{
    private readonly TaskItemService _taskItemService = taskItemService;
    private readonly IMapper _mapper = mapper;

    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    public TaskItemViewModel? viewModel { get; set; } = new();


    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        var dto = await _taskItemService.GetByIdAndOwnerAsync(id, OwnerId);
        if (dto == null)
            return NotFound();

        viewModel = _mapper.Map<TaskItemViewModel>(dto);
        return Page();
    }
}
