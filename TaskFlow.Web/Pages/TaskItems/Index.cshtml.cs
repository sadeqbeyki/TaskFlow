using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Core;
using TaskFlow.Infrastructure;

namespace TaskFlow.Web.Pages.TaskItems
{
    public class IndexModel : PageModel
    {
        private readonly TaskFlowDbContext _context;


        public IndexModel(TaskFlowDbContext context)
        {
            _context = context;
        }


        public IList<TaskItem> Tasks { get; set; } = new List<TaskItem>();


        public async Task OnGetAsync()
        {
            Tasks = await _context.TaskItems
            .Include(t => t.Project)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
        }
    }
}
