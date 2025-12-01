using TaskFlow.Core.Entities;

namespace TaskFlow.Core.Filters;

public class TaskItemFilter
{
    // جستجو
    public string? SearchText { get; set; }
    public bool SearchInDescription { get; set; } = false;

    // فیلترهای انتخابی
    public TaskItemPriority? Priority { get; set; }
    public TaskItemStatus? Status { get; set; }
    public Guid? ProjectId { get; set; }

    // بازه‌های تاریخی
    public DateTime? DueDateFrom { get; set; }
    public DateTime? DueDateTo { get; set; }
    public DateTime? CreatedFrom { get; set; }
    public DateTime? CreatedTo { get; set; }

    // paging
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 7;

    // sorting
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; } = false;

}






