using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Core.Entities;
using TaskFlow.Infrastructure;
using TaskFlow.Web.ViewModels;

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
        public TaskInputModel taskInputModel { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var entity = await _context.TaskItems.FindAsync(id);
            if (entity == null)
                return NotFound();

            Id = entity.Id;
            // Map entity -> input model for showing in form
            taskInputModel.Title = entity.Title;
            taskInputModel.Description = entity.Description;
            taskInputModel.DueDate = entity.DueDate;
            taskInputModel.Priority = entity.Priority;
            taskInputModel.Status = entity.Status;
            taskInputModel.ProjectId = entity.ProjectId;

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var entity = await _context.TaskItems.FindAsync(Id);
            if (entity == null) return NotFound();

            // Use domain method to update (we defined UpdateDetails earlier)
            entity.UpdateDetails(taskInputModel.Title, taskInputModel.Description, taskInputModel.DueDate, taskInputModel.Priority);

            // If you allow changing status from UI, call domain methods appropriately:
            if (taskInputModel.Status == TaskItemStatus.InProgress && entity.Status != TaskItemStatus.InProgress)
                entity.MarkInProgress();
            else if (taskInputModel.Status == TaskItemStatus.Done && entity.Status != TaskItemStatus.Done)
                entity.MarkDone();
            // (else leave as is)

            try
            {
                await _context.SaveChangesAsync();
                TempData["Message"] = "Task updated successfully.";
                return RedirectToPage("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError(string.Empty, "Concurrency issue occurred while updating.");
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return Page();
            }
        }
    }
}
