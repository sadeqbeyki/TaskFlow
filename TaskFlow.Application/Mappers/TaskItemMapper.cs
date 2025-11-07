using TaskFlow.Application.DTOs.TaskItems;
using TaskFlow.Core;

namespace TaskFlow.Application.Mappers;

public static class TaskItemMapper
{
    public static TaskItemDto ToDto(TaskItem t)
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

