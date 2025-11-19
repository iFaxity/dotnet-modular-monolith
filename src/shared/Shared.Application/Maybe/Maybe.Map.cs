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
        /// Maps the value to a new one if present, otherwise returns <c>None</c>.
        /// </summary>
        /// <typeparam name="TResult">The type of the mapped value.</typeparam>
        /// <param name="value">The function to map the value.</param>
        /// <returns>A new maybe with the mapped value or <c>None</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMaybe<TResult> Map<TResult>(TResult? value)
            where TResult : notnull => maybe.Match(() => Maybe.From(value), Maybe<TResult>.None);

        /// <summary>
        /// Maps the value to a new one if present, otherwise returns <c>None</c>.
        /// </summary>
        /// <typeparam name="TResult">The type of the mapped value.</typeparam>
        /// <param name="map">The function to map the value.</param>
        /// <returns>A new maybe with the mapped value or <c>None</c>.</returns>
        public IMaybe<TResult> Map<TResult>(Func<T, TResult?> map)
            where TResult : notnull
        {
            IMaybe<TResult> mapSome(T value)
            {
                var mappedValue = map(value);

                return Maybe.From(mappedValue);
            }

            return maybe.Match(mapSome, Maybe<TResult>.None);
        }

        /// <summary>
        /// Asynchronously maps the value to a new one if present, otherwise returns <c>None</c>.
        /// </summary>
        /// <typeparam name="TResult">The type of the mapped value.</typeparam>
        /// <param name="value">The asynchronous function to map the value.</param>
        /// <returns>A task returning a maybe with the mapped value or <c>None</c>.</returns>
        public Task<IMaybe<TResult>> Map<TResult>(Task<TResult?> value)
            where TResult : notnull
        {
            async Task<IMaybe<TResult>> mapSome()
            {
                var mappedValue = await value;

                return Maybe.From(mappedValue);
            }

            return maybe.Match(mapSome, Maybe<TResult>.None);
        }

        /// <summary>
        /// Asynchronously maps the value to a new one if present, otherwise returns <c>None</c>.
        /// </summary>
        /// <typeparam name="TResult">The type of the mapped value.</typeparam>
        /// <param name="map">The asynchronous function to map the value.</param>
        /// <returns>A task returning a maybe with the mapped value or <c>None</c>.</returns>
        public Task<IMaybe<TResult>> Map<TResult>(Func<T, Task<TResult?>> map)
            where TResult : notnull
        {
            async Task<IMaybe<TResult>> mapSome(T value)
            {
                var mappedValue = await map(value);

                return Maybe.From(mappedValue);
            }

            return maybe.Match(mapSome, Maybe<TResult>.None);
        }
    }
}
