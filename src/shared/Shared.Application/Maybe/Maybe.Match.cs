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
        /// Matches a maybe value to a result or returning a fallback value.
        /// </summary>
        /// <typeparam name="T">The type of the maybe value.</typeparam>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="maybe">The maybe instance.</param>
        /// <param name="some">Result if value is present.</param>
        /// <param name="none">Fallback value if no value is present.</param>
        /// <returns>The result of the match.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult Match<TResult>(TResult some, TResult none) => maybe.IsSome ? some : none;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult Match<TResult>(TResult some, Func<TResult> mapNone) =>
            maybe.IsSome ? some : mapNone();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult Match<TResult>(Func<TResult> mapSome, TResult none) =>
            maybe.IsSome ? mapSome() : none;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult Match<TResult>(Func<TResult> mapSome, Func<TResult> mapNone) =>
            maybe.IsSome ? mapSome() : mapNone();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult Match<TResult>(Func<T, TResult> mapSome, TResult none)
        {
            if (!Maybe.TryUnwrap(maybe, out var value))
                return none;

            return mapSome(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult Match<TResult>(Func<T, TResult> mapSome, Func<TResult> mapNone)
        {
            if (!Maybe.TryUnwrap(maybe, out var value))
                return mapNone();

            return mapSome(value);
        }

        /// <summary>
        /// Asynchronously matches a maybe value to a result or returns a fallback value.
        /// </summary>
        /// <typeparam name="T">The type of the maybe value.</typeparam>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="maybe">The maybe instance.</param>
        /// <param name="mapSome">Async function to execute if value is present.</param>
        /// <param name="none">Fallback result if value is not present.</param>
        /// <returns>A task returning the match result.</returns>
        public Task<TResult> Match<TResult>(Func<T, Task<TResult>> mapSome, TResult none) =>
            maybe.Match(mapSome, () => Task.FromResult(none));

        /// <summary>
        /// Asynchronously matches a maybe value to a result by executing one of two functions.
        /// </summary>
        /// <typeparam name="T">The type of the maybe value.</typeparam>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="maybe">The maybe instance.</param>
        /// <param name="mapSome">Async function if value is present.</param>
        /// <param name="mapNone">Sync function if no value is present.</param>
        /// <returns>A task returning the match result.</returns>
        public Task<TResult> Match<TResult>(
            Func<T, Task<TResult>> mapSome,
            Func<TResult> mapNone
        ) => maybe.Match(mapSome, () => Task.FromResult(mapNone()));

        /// <summary>
        /// Fully asynchronous match operation using asynchronous branches.
        /// </summary>
        /// <typeparam name="T">The type of the maybe value.</typeparam>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="maybe">The maybe instance.</param>
        /// <param name="mapSome">Async function to call if value is present.</param>
        /// <param name="mapNone">Async function to call if value is not present.</param>
        /// <returns>A task returning the match result.</returns>
        public Task<TResult> Match<TResult>(
            Func<T, Task<TResult>> mapSome,
            Func<Task<TResult>> mapNone
        )
        {
            if (!Maybe.TryUnwrap(maybe, out var value))
                return mapNone();

            return mapSome(value);
        }

        /// <summary>
        /// Matches a maybe value and executes an async fallback if <c>None</c>.
        /// </summary>
        /// <typeparam name="T">The type of the maybe value.</typeparam>
        /// <typeparam name="TResult">The type of the result value.</typeparam>
        /// <param name="maybe">The maybe instance.</param>
        /// <param name="mapSome">Function to call if value is present.</param>
        /// <param name="mapNone">Async function to call if value is not present.</param>
        /// <returns>A task returning the match result.</returns>
        public Task<TResult> Match<TResult>(Func<T, TResult> mapSome, Func<Task<TResult>> mapNone)
        {
            if (!Maybe.TryUnwrap(maybe, out var value))
                return mapNone();

            return Task.FromResult(mapSome(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<TResult> Match<TResult>(Func<Task<TResult>> mapSome, TResult none) =>
            maybe.IsSome ? mapSome() : Task.FromResult(none);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<TResult> Match<TResult>(TResult some, Func<Task<TResult>> mapNone) =>
            maybe.IsSome ? Task.FromResult(some) : mapNone();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<TResult> Match<TResult>(
            Func<Task<TResult>> mapSome,
            Func<Task<TResult>> mapNone
        ) => maybe.IsSome ? mapSome() : mapNone();
    }
}
