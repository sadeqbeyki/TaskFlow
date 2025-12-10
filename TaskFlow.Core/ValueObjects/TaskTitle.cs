using TaskFlow.Core.Exceptions;
namespace TaskFlow.Core.ValueObjects;

public sealed class TaskTitle : IEquatable<TaskTitle>
{
    private const int MaxLength = 250;

    private readonly string _value;
    public string Value => _value;

    public TaskTitle(string value)
    {
        if (value is null)
            throw new DomainException("Task title cannot be null.");

        var normalized = value.Trim();

        if (normalized.Length == 0)
            throw new DomainException("Task title cannot be empty.");

        if (normalized.Length > MaxLength)
            throw new DomainException($"Task title cannot be longer than {MaxLength} characters.");

        _value = normalized;
    }

    public static TaskTitle From(string value)
        => new(value);

    public override string ToString()
        => Value;

    #region Equality

    public bool Equals(TaskTitle? other)
    {
        if (other is null) return false;
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
        => Equals(obj as TaskTitle);

    public override int GetHashCode()
        => Value.GetHashCode();

    public static bool operator ==(TaskTitle? left, TaskTitle? right)
        => Equals(left, right);

    public static bool operator !=(TaskTitle? left, TaskTitle? right)
        => !Equals(left, right);

    #endregion
}

