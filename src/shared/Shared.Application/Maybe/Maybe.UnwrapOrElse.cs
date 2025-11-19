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
        /// Gets the value or returns the value from the provided function if <c>None</c>.
        /// </summary>
        /// <typeparam name="T">The type of the maybe value.</typeparam>
        /// <param name="maybe">The maybe instance.</param>
        /// <param name="mapDefault">The function returning the default value.</param>
        /// <returns>The maybe value or the value from the function.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T UnwrapOrElse(Func<T> mapDefault) => maybe.Match((value) => value, mapDefault);

        /// <summary>
        /// Asynchronously gets the value or returns the value from the provided async function if <c>None</c>.
        /// </summary>
        /// <typeparam name="T">The type of the maybe value.</typeparam>
        /// <param name="maybe">The maybe instance.</param>
        /// <param name="mapDefault">The asynchronous function returning the default value.</param>
        /// <returns>A task returning the value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<T> UnwrapOrElse(Func<Task<T>> mapDefault) =>
            maybe.Match((value) => Task.FromResult(value), mapDefault);
    }
}
