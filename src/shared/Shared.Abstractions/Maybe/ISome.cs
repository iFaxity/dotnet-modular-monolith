namespace Shared.Abstractions;

public interface ISome<out T> : IMaybe<T>
    where T : notnull
{
    T Value { get; }
}
