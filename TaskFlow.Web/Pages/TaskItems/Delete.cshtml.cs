using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskFlow.Core;
using TaskFlow.Infrastructure;

namespace TaskFlow.Web.Pages.TaskItems
{
    public class DeleteModel : PageModel
    {
        private readonly TaskFlowDbContext _context;


        public DeleteModel(TaskFlowDbContext context)
        {
            _context = context;
        }


        [BindProperty]
        public TaskItem TaskItem { get; set; } = new();


        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            TaskItem = await _context.TaskItems.FindAsync(id) ?? new TaskItem();
            if (TaskItem.Id == Guid.Empty)
                return NotFound();


            return Page();
        }


        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            try
            {


                var task = await _context.TaskItems.FindAsync(id);
                if (task != null)
                {
                    _context.TaskItems.Remove(task);
                    await _context.SaveChangesAsync();
                }


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
