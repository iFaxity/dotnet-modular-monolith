using System.Collections;

namespace Shared.Application;

/// <summary>
/// Represents an optional value that may or may not be present.
/// </summary>
/// <typeparam name="T">The type of the optional value.</typeparam>
public readonly record struct Maybe<T> : IMaybe<T>
    where T : notnull
{
    /// <summary>
    /// Gets a <see cref="IMaybe{T}"/> instance that represents the absence of a value.
    /// </summary>
    public static readonly IMaybe<T> None = new Maybe<T>();

    private readonly T? _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Maybe{T}"/> struct.
    /// </summary>
    /// <param name="value">The value to wrap. If <c>null</c>, the instance will represent <c>None</c>.</param>
    internal Maybe(T? value)
    {
        if (value is null)
            return;

        _value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Maybe{T}"/> struct.
    /// </summary>
    /// <param name="maybe">The maybe to copy.</param>
    internal Maybe(IMaybe<T> maybe)
    {
        if (Maybe.TryUnwrap(maybe, out var value))
            _value = value;
    }

    /// <summary>
    /// Gets a value indicating whether the current instance contains a value.
    /// </summary>
    public bool IsSome => _value is not null;

    /// <summary>
    /// Gets a value indicating whether the current instance does not contain a value.
    /// </summary>
    public bool IsNone => _value is null;

    /// <summary>
    /// Implicitly converts a value of type <typeparamref name="T"/> to a <see cref="Maybe{T}"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static implicit operator Maybe<T>(T? value)
    {
        return new(value);
    }

    /// <summary>
    /// Returns the value if the maybe is Some, otherwise throws an exception.
    /// </summary>
    /// <returns>The contained value.</returns>
    /// <exception cref="MaybeNoneException"> when <see cref="IsNone"/> is <c>true</c>.</exception>
    public T Unwrap()
    {
        return _value ?? throw new MaybeNoneException();
    }

    /// <summary>
    /// Returns an enumerator that iterates through the value if it is present.
    /// </summary>
    /// <returns>An enumerator for the value or an empty enumerator if <c>None</c>.</returns>
    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        if (_value is null)
            yield break;

        yield return _value;
    }

    /// <summary>
    /// Returns a non-generic enumerator that iterates through the value if it is present.
    /// </summary>
    /// <returns>An enumerator for the value or an empty enumerator if <c>None</c>.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<T>)this).GetEnumerator();
    }

    /// <summary>
    /// Returns a string representation of the <see cref="Maybe{T}"/>.
    /// </summary>
    /// <returns>A string representing the value.</returns>
    public override string ToString()
    {
        return _value is null ? $"None<{typeof(T).Name}>()" : $"Some({_value})";
    }
}
