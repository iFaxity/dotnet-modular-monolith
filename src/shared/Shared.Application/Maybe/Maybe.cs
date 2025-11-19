using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Shared.Application;

public static class Maybe
{
    internal static bool TryUnwrap<T>(IMaybe<T> maybe, [NotNullWhen(true)] out T? value)
        where T : notnull
    {
        value = default;

        if (maybe.IsNone)
            return false;

        value = maybe.Unwrap()!;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IMaybe<T> From<T>(T? value)
        where T : notnull
    {
        return new Maybe<T>(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IMaybe<T> From<T>(IMaybe<T> maybe)
        where T : notnull
    {
        return new Maybe<T>(maybe);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IMaybe<T> From<T>(Func<T?> map)
        where T : notnull
    {
        return new Maybe<T>(map());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IMaybe<T> Some<T>(T value)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(value);

        return new Maybe<T>(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IMaybe<T> Some<T>(IMaybe<T> maybe)
        where T : notnull
    {
        if (maybe.IsNone)
            throw new MaybeNoneException();

        return new Maybe<T>(maybe);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IMaybe<T> Some<T>(Func<T?> map)
        where T : notnull
    {
        return new Maybe<T>(map());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IMaybe<T> None<T>()
        where T : notnull
    {
        return Maybe<T>.None;
    }
}
