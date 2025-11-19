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
        public IMaybe<T> Ensure(Func<T, bool> predicate)
        {
            if (!Maybe.TryUnwrap(maybe, out var value))
                return maybe;

            var match = predicate(value);

            return match ? maybe : Maybe.None<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IMaybe<T>> Ensure(Func<T, Task<bool>> predicate)
        {
            if (!Maybe.TryUnwrap(maybe, out var value))
                return maybe;

            var match = await predicate(value);

            return match ? maybe : Maybe.None<T>();
        }
    }
}
