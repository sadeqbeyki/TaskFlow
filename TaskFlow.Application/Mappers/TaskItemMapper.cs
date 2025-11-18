using TaskFlow.Application.DTOs.Projects;
using TaskFlow.Application.DTOs.TaskItems;
using TaskFlow.Core.Entities;

namespace TaskFlow.Application.Mappers;

public static class TaskItemMapper
{
    public static TaskItemDto MapToDto(TaskItem t)
    {
        if (t == null) return null!; // caller باید null-check کند یا از nullable return استفاده کند

        return new TaskItemDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            DueDate = t.DueDate,
            Priority = t.Priority,
            Status = t.Status,
            ProjectId = t.ProjectId,
            CreatedAt = t.CreatedAt,
            UpdatedAt = t.UpdatedAt
        };
    }

    // اگر بخواهی می‌توانی توابع معکوس هم اضافه کنی (از DTO -> Entity) ولی
    // برای DDD ترجیحاً ایجاد Entity با سازنده دامنه انجام شود (نه Map مستقیم).
}



public static class ProjectMapper
{
    public static ProjectDto MapToDto(Project p)
    {
        if (p == null) return null!; 

        return new ProjectDto
        {
            Id = p.Id.ToString(),
            Title = p.Title,
            Description = p.Description,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt
        };
    }
}