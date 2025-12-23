using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Application.UseCases.Projects.Delete;

public sealed class DeleteProjectCommand
{
    public Guid ProjectId { get; }

    public DeleteProjectCommand(Guid projectId)
    {
        ProjectId = projectId;
    }
}
