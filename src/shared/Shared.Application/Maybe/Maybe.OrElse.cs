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
        public IMaybe<T> OrElse(Func<IMaybe<T>> functor) => maybe.Match(() => maybe, functor);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<IMaybe<T>> OrElse(Func<Task<IMaybe<T>>> functor) =>
            maybe.Match(Task.FromResult(maybe), functor);
    }
}
