namespace Shared.Abstractions;

public interface IMaybe<out T> : IEnumerable<T>
    where T : notnull, allows ref struct
{
    bool IsSome { get; }
    bool IsNone { get; }
    T Unwrap();
}
