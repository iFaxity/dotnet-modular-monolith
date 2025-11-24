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
            if (maybe is not ISome<T> some)
                return maybe;

            var match = predicate(some.Value);

            return match ? maybe : Maybe.None<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IMaybe<T>> Ensure(Func<T, Task<bool>> predicate)
        {
            if (maybe is not ISome<T> some)
                return maybe;

            var match = await predicate(some.Value);

            return match ? maybe : Maybe.None<T>();
        }
    }
}
