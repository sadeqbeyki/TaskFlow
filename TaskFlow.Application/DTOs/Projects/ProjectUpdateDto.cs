namespace TaskFlow.Application.DTOs.Projects;

public class ProjectUpdateDto
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
