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
        public bool Satisfies(Func<T, bool> predicate) => maybe.Match(predicate, false);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<bool> Satisfies(Func<T, Task<bool>> predicate) => maybe.Match(predicate, false);
    }
}
