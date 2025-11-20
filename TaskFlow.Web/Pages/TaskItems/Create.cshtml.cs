using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskFlow.Application.DTOs.TaskItems;
using TaskFlow.Application.Interfaces;
using TaskFlow.Web.ViewModels;

namespace TaskFlow.Web.Pages.TaskItems
{
    public class CreateModel : PageModel
    {
        private readonly ITaskItemService _taskItemService;
        private Guid ownerId;

        public CreateModel(ITaskItemService taskItemService)
        {
            _taskItemService = taskItemService;
        }

        [BindProperty]
        public TaskInputModel taskItemModel { get; set; } = new();


        public IActionResult OnGet()
        {
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var task = new TaskItemCreateDto{
                Title = taskItemModel.Title,
                Description = taskItemModel.Description,
                DueDate = taskItemModel.DueDate,
                Priority = taskItemModel.Priority,
                ProjectId = taskItemModel.ProjectId,
            };

            try
            {
                await _taskItemService.CreateAsync(task, ownerId);
                TempData["Message"] = "Task created successfully.";
                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error has occurred.: {ex.Message}");
                return Page();
            }
        }
    }
}
