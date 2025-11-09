using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.DTOs.TaskItems;

namespace TaskFlow.Application.Interfaces;

internal interface ITaskItemService
{
    Task<TaskItemDto?> GetByIdAsync(Guid id, Guid ownerId);
    Task<List<TaskItemDto>> GetAllByProjectAsync(Guid projectId, Guid ownerId);
}
