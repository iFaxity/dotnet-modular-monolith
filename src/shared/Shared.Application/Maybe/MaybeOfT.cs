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
    public static readonly IMaybe<T> None = new Maybe<T>(Application.None.Instance);

    private readonly IMaybe<T> _state;

    internal Maybe(INone none)
    {
        _state = (IMaybe<T>)none;
    }

    internal Maybe(IMaybe<T> maybe)
    {
        // Unwrap the error to be a correct copy constructor and avoid nested maybes
        _state = maybe switch
        {
            ISome<T> some => new Some<T>(some.Value),
            INone => None,
            _ => throw new InvalidOperationException(),
        };
    }

    internal Maybe(T? value)
    {
        _state = value is null ? None : new Some<T>(value);
    }

    public bool IsSome => _state.IsSome;
    public bool IsNone => _state.IsNone;

    /// <summary>
    /// Implicitly converts a value of type <typeparamref name="T"/> to a <see cref="Maybe{T}"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static implicit operator Maybe<T>(T? value)
    {
        if (value is null)
            return new(Application.None.Instance);

        return new(value);
    }

    /// <summary>
    /// Returns a string representation of the <see cref="Maybe{T}"/>.
    /// </summary>
    /// <returns>A string representing the value.</returns>
    public override string? ToString()
    {
        return _state switch
        {
            ISome<T> some => $"Some({some.Value})",
            INone => $"None<{typeof(T).Name}>()",
            _ => throw new InvalidOperationException(),
        };
    }
}
