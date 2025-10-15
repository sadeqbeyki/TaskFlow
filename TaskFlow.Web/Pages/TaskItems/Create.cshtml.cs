using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Core;
using TaskFlow.Infrastructure;
using TaskFlow.Web.ViewModels;

namespace TaskFlow.Web.Pages.TaskItems
{
    public class CreateModel : PageModel
    {
        private readonly TaskFlowDbContext _context;


        public CreateModel(TaskFlowDbContext context)
        {
            _context = context;
        }


        [BindProperty]
        public TaskInputModel taskItem { get; set; } = new();


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

            var task = new TaskItem(
                title: taskItem.Title,
                description: taskItem.Description,
                dueDate: taskItem.DueDate,
                priority: taskItem.Priority,
                projectId: taskItem.ProjectId
            );


            try
            {
                _context.TaskItems.Add(task);
                await _context.SaveChangesAsync();
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
