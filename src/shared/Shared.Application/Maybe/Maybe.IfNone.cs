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
        public IMaybe<T> IfNone(Action action)
        {
            if (maybe.IsNone)
                action();

            return maybe;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IMaybe<T>> IfNone(Func<Task> action)
        {
            if (maybe.IsNone)
                await action();

            return maybe;
        }
    }

    extension<T>(IEnumerable<IMaybe<T>> maybes)
        where T : notnull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IMaybe<T>> IfNone(Action action)
        {
            foreach (var maybe in maybes)
                maybe.IfNone(action);

            return maybes;
        }
    }
}
