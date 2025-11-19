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
        /// <summary>
        /// Binds the maybe to a new maybe by applying the provided function.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting maybe value.</typeparam>
        /// <param name="maybe">The maybe instance.</param>
        /// <param name="functor">The function that returns a new maybe.</param>
        /// <returns>The resulting maybe or <c>None</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMaybe<TResult> AndThen<TResult>(Func<T, IMaybe<TResult>> functor)
            where TResult : notnull => maybe.Match(functor, Maybe<TResult>.None);

        /// <summary>
        /// Asynchronously binds the maybe to a new maybe.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting maybe value.</typeparam>
        /// <param name="maybe">The maybe instance.</param>
        /// <param name="functor">The asynchronous function returning a new maybe.</param>
        /// <returns>A task returning the resulting maybe or <c>None</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<IMaybe<TResult>> AndThen<TResult>(Func<T, Task<IMaybe<TResult>>> functor)
            where TResult : notnull => maybe.Match(functor, Maybe<TResult>.None);
    }
}
