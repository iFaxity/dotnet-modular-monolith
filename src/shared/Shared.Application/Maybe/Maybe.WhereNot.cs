using System.Runtime.CompilerServices;

namespace Shared.Application;

/// <summary>
/// Extension methods for the <see cref="IMaybe{T}"/> type.
/// </summary>
public static partial class MaybeExtensions
{
    extension<T>(IEnumerable<IMaybe<T>> maybes)
        where T : notnull
    {
        /// <summary>
        /// Filters <c>Some</c>s via negated predicate.
        /// <c>None</c>s are discarded.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IMaybe<T>> WhereNot(Func<T, bool> predicate) =>
            Where(maybes, v => !predicate(v));
    }
}
