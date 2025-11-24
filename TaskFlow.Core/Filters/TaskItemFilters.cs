using TaskFlow.Core.Entities;

namespace TaskFlow.Application.Filters;



public sealed class TaskItemFilter
{
    public string? Search { get; set; }
    //public bool? IsDone { get; set; }
    public DateTime? CreatedFrom { get; set; }
    public DateTime? CreatedTo { get; set; }
    public DateTime? DueFrom { get; set; }
    public DateTime? DueTo { get; set; }
    public string? OrderBy { get; set; }
    public bool Desc { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}




