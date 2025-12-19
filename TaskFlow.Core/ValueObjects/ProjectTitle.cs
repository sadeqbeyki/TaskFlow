using TaskFlow.Core.Exceptions;

namespace TaskFlow.Core.ValueObjects;

public sealed class ProjectTitle : IEquatable<ProjectTitle>
{
    private const int MaxLength = 100;

    private readonly string _value;
    public string Value => _value;

    public ProjectTitle(string value)
    {
        if (value is null)
            throw new DomainException("Project title cannot be null.");

        var normalized = value.Trim();

        if (normalized.Length == 0)
            throw new DomainException("Project title cannot be empty.");

        if (normalized.Length > MaxLength)
            throw new DomainException($"Project title cannot exceed {MaxLength} characters.");

        _value = normalized;
    }

    public override string ToString() => Value;

    #region Equality
    public bool Equals(ProjectTitle? other)
        => other is not null && Value == other.Value;

    public override bool Equals(object? obj)
        => Equals(obj as ProjectTitle);

    public override int GetHashCode()
        => Value.GetHashCode();
    #endregion
}