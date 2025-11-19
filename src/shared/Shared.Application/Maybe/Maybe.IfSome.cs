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
        /// Executes an action if the maybe has a value.
        /// </summary>
        /// <typeparam name="T">The type of the maybe value.</typeparam>
        /// <param name="maybe">The maybe instance.</param>
        /// <param name="action">The action to execute.</param>
        /// <returns>The original maybe instance.</returns>
        public IMaybe<T> IfSome(Action<T> action)
        {
            IMaybe<T> mapSome(T value)
            {
                action(value);
                return maybe;
            }

            return maybe.Match(mapSome, maybe);
        }

        /// <summary>
        /// Executes an async action if the maybe has a value.
        /// </summary>
        /// <typeparam name="T">The type of the maybe value.</typeparam>
        /// <param name="maybe">The maybe instance.</param>
        /// <param name="action">The asynchronous action to execute.</param>
        /// <returns>A task returning the original maybe instance.</returns>
        public Task<IMaybe<T>> IfSome(Func<T, Task> action)
        {
            async Task<IMaybe<T>> mapSome(T value)
            {
                await action(value);
                return maybe;
            }

            return maybe.Match(mapSome, () => Task.FromResult(maybe));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMaybe<T> IfSome(Action action)
        {
            if (maybe.IsSome)
                action();

            return maybe;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IMaybe<T>> IfSome(Func<Task> action)
        {
            if (maybe.IsSome)
                await action();

            return maybe;
        }
    }

    extension<T>(IEnumerable<IMaybe<T>> maybes)
        where T : notnull
    {
        public IEnumerable<IMaybe<T>> IfSome(Action<T> action)
        {
            foreach (var maybe in maybes)
                maybe.IfSome(action);

            return maybes;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IMaybe<T>> IfSome(Action action)
        {
            foreach (var maybe in maybes)
                maybe.IfSome(action);

            return maybes;
        }
    }
}
