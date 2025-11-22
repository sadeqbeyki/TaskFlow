using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskFlow.Application.Services;
using TaskFlow.Web.Pages.TaskItems.Models;

namespace TaskFlow.Web.Pages.TaskItems
{
    public class DetailsModel : PageModel
    {
        private readonly TaskItemService _taskItemService;
        private readonly IMapper _mapper;

        public DetailsModel(TaskItemService taskItemService, IMapper mapper)
        {
            _taskItemService = taskItemService;
            _mapper = mapper;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        public TaskItemViewModel? viewModel { get; set; } = new();


        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var ownerId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var dto = await _taskItemService.GetByIdAndOwnerAsync(id, ownerId);
            if (dto == null)
                return NotFound();

            viewModel = _mapper.Map<TaskItemViewModel>(dto);
            return Page();
        }
    }
}
