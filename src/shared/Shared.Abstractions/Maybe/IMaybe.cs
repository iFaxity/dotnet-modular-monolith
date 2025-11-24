namespace Shared.Abstractions;

public interface IMaybe<out T>
    where T : notnull
{
    /// <summary>
    /// Gets a value indicating whether the current instance contains a value.
    /// </summary>
    bool IsSome { get; }

    /// <summary>
    /// Gets a value indicating whether the current instance does not contain a value.
    /// </summary>
    bool IsNone { get; }
}
