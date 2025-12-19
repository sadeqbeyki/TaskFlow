using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Application.DTOs.Projects;

public class ProjectUpdateDto
{
    [Required(ErrorMessage = "Project title is required.")]
    [StringLength(100, ErrorMessage = "Title must not exceed 100 characters.")]
    public string Title { get; set; } = default!;
    [StringLength(500, ErrorMessage = "Description must not exceed 500 characters.")]
    public string? Description { get; set; }
}
