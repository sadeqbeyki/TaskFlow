using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskFlow.Core.ValueObjects;

namespace TaskFlow.Infrastructure.Persistence.Converters;


public class TaskTitleConverter : ValueConverter<TaskTitle, string>
{
    public TaskTitleConverter()
        : base(
            title => title.Value,
            value => new TaskTitle(value))
    {
    }
}

