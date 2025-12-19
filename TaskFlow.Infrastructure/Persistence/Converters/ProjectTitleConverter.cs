using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskFlow.Core.ValueObjects;

namespace TaskFlow.Infrastructure.Persistence.Converters;

public class ProjectTitleConverter : ValueConverter<ProjectTitle, string>
{
    public ProjectTitleConverter()
        : base(
            title => title.Value,
            value => new ProjectTitle(value))
    {
    }
}
