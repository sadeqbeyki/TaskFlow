using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Application.UseCases.Projects.GetById;

public sealed class GetProjectByIdQuery
{
    public Guid ProjectId { get; }

    public GetProjectByIdQuery(Guid projectId)
    {
        ProjectId = projectId;
    }
}
