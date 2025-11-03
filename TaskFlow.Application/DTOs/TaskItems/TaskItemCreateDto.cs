using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Core;

namespace TaskFlow.Application.DTOs.TaskItems;

// For creating a new TaskItem
public class TaskItemCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public TaskItemPriority Priority { get; set; } = TaskItemPriority.Medium;
    public Guid ProjectId { get; set; } // The project this task belongs to
}

