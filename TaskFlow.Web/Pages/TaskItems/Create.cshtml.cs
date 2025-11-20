using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using TaskFlow.Application.DTOs.TaskItems;
using TaskFlow.Application.Interfaces;
using TaskFlow.Web.ViewModels;

namespace TaskFlow.Web.Pages.TaskItems
{
    public class CreateModel : PageModel
    {
        private readonly ITaskItemService _taskItemService;
        private readonly IProjectService _projectService;

        public CreateModel(ITaskItemService taskService, IProjectService projectService)
        {
            _taskItemService = taskService;
            _projectService = projectService;
        }
        private readonly Guid _fakeOwnerId = Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee");

        [BindProperty]
        public TaskItemCreateDto taskItemModel { get; set; } = new();

        public SelectList ProjectList { get; set; } = new SelectList(new List<SelectListItem>());


        public async Task<IActionResult> OnGetAsync(Guid? projectId)
        {
            var ownerId = GetCurrentUserId();

            var projects = await _projectService.GetAllByUserAsync(_fakeOwnerId);
            ProjectList = new SelectList(projects, "Id", "Title", projectId?.ToString());

            if (projectId.HasValue)
                taskItemModel.ProjectId = projectId.Value;

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var ownerId = GetCurrentUserId();

            try
            {
                await _taskItemService.CreateAsync(taskItemModel, ownerId);
                TempData["Message"] = "Task created successfully.";
                return RedirectToPage("/Tasks/Index", new { projectId = taskItemModel.ProjectId });
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
