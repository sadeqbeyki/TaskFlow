using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Application.DTOs.Projects;

public class ProjectCreateDto
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
}
