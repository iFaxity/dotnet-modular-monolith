namespace Shared.Application;

internal sealed class Some<T>(T value) : ISome<T>
    where T : notnull
{
    public T Value { get; } = value;
    public bool IsSome => true;
    public bool IsNone => false;
}
