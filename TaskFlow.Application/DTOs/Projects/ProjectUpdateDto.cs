namespace TaskFlow.Application.DTOs.Projects;

public class ProjectUpdateDto
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
}
