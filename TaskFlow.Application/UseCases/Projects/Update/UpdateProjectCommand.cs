using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Application.UseCases.Projects.Update;

public sealed class UpdateProjectCommand
{
    public Guid ProjectId { get; }
    public string Title { get; }
    public string? Description { get; }

    public UpdateProjectCommand(
        Guid projectId,
        string title,
        string? description)
    {
        ProjectId = projectId;
        Title = title;
        Description = description;
    }
}
