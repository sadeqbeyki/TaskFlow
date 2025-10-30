namespace TaskFlow.Application.DTOs.Projects;

public class ProjectTaskSummaryDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public bool IsDone { get; set; }
    public DateTime? DueDate { get; set; }
}
