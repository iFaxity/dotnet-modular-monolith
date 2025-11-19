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
        /// Determines if any <see cref="IMaybe{T}"/>s of a sequence are <c>Some</c>s.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AnySome() => maybes.Any(m => m.IsSome);
    }
}
