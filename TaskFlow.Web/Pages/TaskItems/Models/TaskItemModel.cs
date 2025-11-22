using TaskFlow.Core.Entities;

namespace TaskFlow.Web.Pages.TaskItems.Models
{
    public class TaskItemModel
    {
        public string Id { get; set; } = "";

        public string ProjectId { get; set; } = "";

        // Editable Fields
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }

        public TaskItemPriority Priority { get; set; }
        public TaskItemStatus Status { get; set; }

        // Read-only display fields
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
