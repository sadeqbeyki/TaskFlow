using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using TaskFlow.Application.DTOs.TaskItems;
using TaskFlow.Application.Interfaces;
using TaskFlow.Web.Pages.TaskItems.Models;

namespace TaskFlow.Web.Pages.TaskItems
{
    public class CreateModel : PageModel
    {
        private readonly ITaskItemService _taskItemService;
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public CreateModel(ITaskItemService taskService, IProjectService projectService, IMapper mapper)
        {
            _taskItemService = taskService;
            _projectService = projectService;
            _mapper = mapper;
        }

        [BindProperty]
        public TaskItemInputModel inputModel { get; set; } = new();
        public SelectList ProjectList { get; set; } = new SelectList(new List<SelectListItem>());


        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        private readonly Guid _fakeOwnerId = Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee");

        public async Task<IActionResult> OnGetAsync(Guid? projectId)
        {
            var projects = await _projectService.GetAllByUserAsync(_fakeOwnerId);
            ProjectList = new SelectList(projects, "Id", "Title", projectId?.ToString());

            if (projectId.HasValue)
                inputModel.ProjectId = projectId.Value;

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            Guid ownerId = _fakeOwnerId;
            var taskItem = _mapper.Map<TaskItemCreateDto>(inputModel);

            try
            {
                var id = await _taskItemService.CreateAsync(taskItem, ownerId);
                TempData["Message"] = "Task created successfully.";
                return RedirectToPage("Index");
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        private Guid GetCurrentUserId()
        {
            var id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(id, out var guid))
                throw new InvalidOperationException("User not authenticated.");
            return guid;
        }
    }
}
