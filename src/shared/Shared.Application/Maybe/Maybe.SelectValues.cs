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
        /// Extract values from <see cref="IMaybe{T}"/>s.
        /// <c>None</c>s are discarded.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> SelectValues() =>
            maybes.OfType<ISome<T>>().Select(some => some.Value);
    }
}
