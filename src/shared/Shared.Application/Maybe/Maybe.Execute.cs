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
        public IMaybe<T> Execute(Action action)
        {
            action();

            return maybe;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMaybe<T> Execute(Action<IMaybe<T>> action)
        {
            action(maybe);

            return maybe;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IMaybe<T>> Execute(Func<Task> action)
        {
            await action();

            return maybe;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IMaybe<T>> Execute(Func<IMaybe<T>, Task> action)
        {
            await action(maybe);

            return maybe;
        }
    }
}
