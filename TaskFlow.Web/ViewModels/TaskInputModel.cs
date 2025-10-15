using System.ComponentModel.DataAnnotations;
using TaskFlow.Core;

namespace TaskFlow.Web.ViewModels;


public class TaskInputModel
{
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(100, ErrorMessage = "Title must not exceed 100 characters.")]
    public string Title { get; set; } = null!;

    [StringLength(500, ErrorMessage = "Description must not exceed 500 characters.")]
    public string? Description { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DueDate { get; set; }

    public TaskItemPriority Priority { get; set; } = TaskItemPriority.Medium;

    public TaskItemStatus Status { get; set; } = TaskItemStatus.Todo;

    public Guid ProjectId { get; set; } // fill or default if you don't use Project yet
}

