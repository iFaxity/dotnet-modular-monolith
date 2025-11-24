namespace Shared.Application;

internal sealed class Success<T>(T value) : ISuccess<T>
    where T : notnull
{
    public bool IsSuccess => true;
    public bool IsFailure => false;
    public T Value { get; } = value;

    /// <summary>
    /// Returns a string representation of the <see cref="Success"/>.
    /// </summary>
    /// <returns>A string representing the value.</returns>
    public override string? ToString() => $"Success({Value})";
}
