using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskFlow.Core;
using TaskFlow.Infrastructure;

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
        public TaskItem TaskItem { get; set; } = new();


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


            //TaskItem.CreatedAt = DateTime.UtcNow;
            //_context.TaskItems.Add(TaskItem);
            //await _context.SaveChangesAsync();


            //return RedirectToPage("Index");


            try
            {
                _context.Add(TaskItem);
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
