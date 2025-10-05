using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Core;
using TaskFlow.Infrastructure;

namespace TaskFlow.Web.Pages.TaskItems
{
    public class EditModel : PageModel
    {
        private readonly TaskFlowDbContext _context;


        public EditModel(TaskFlowDbContext context)
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


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                _context.Attach(TaskItem).State = EntityState.Modified;
                TaskItem.UpdatedAt = DateTime.UtcNow;


                await _context.SaveChangesAsync();
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
