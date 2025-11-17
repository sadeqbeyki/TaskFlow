using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Core.Entities;
using TaskFlow.Infrastructure;

namespace TaskFlow.Web.Pages.TaskItems
{
    public class DetailsModel : PageModel
    {
        private readonly TaskFlowDbContext _context;


        public DetailsModel(TaskFlowDbContext context)
        {
            _context = context;
        }


        public TaskItem TaskItem { get; set; } = new();


        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            TaskItem = await _context.TaskItems.Include(t => t.Project).FirstOrDefaultAsync(t => t.Id == id) ?? new TaskItem();
            if (TaskItem.Id == Guid.Empty)
                return NotFound();


            return Page();
        }
    }
}
