using System.Runtime.CompilerServices;

namespace Shared.Application;

/// <summary>
/// Extension methods for the <see cref="IMaybe{T}"/> type.
/// </summary>
public static partial class MaybeExtensions
{
    extension<T>(IMaybe<T> maybe)
        where T : notnull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IResult<T> ToResult() =>
            maybe.Match(Result.Success, () => Result.Failure<T>(MaybeNoneError.Default));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IResult<T> ToResult(IError error) =>
            maybe.Match(Result.Success, () => Result.Failure<T>(error));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IResult<T> ToResult(Func<IError> selector) =>
            maybe.Match(Result.Success, () => Result.Failure<T>(selector()));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<IResult<T>> ToResult(Func<Task<IError>> selector) =>
            maybe.Match(Result.Success, async () => Result.Failure<T>(await selector()));
    }
}
