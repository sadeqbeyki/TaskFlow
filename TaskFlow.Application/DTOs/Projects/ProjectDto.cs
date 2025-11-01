namespace TaskFlow.Application.DTOs.Projects;

public class ProjectDto
{
    public string Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public List<ProjectTaskSummaryDto> Tasks { get; set; } = new();
}
