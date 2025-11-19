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
        /// Determines if all <see cref="IMaybe{T}"/>s of a sequence are <c>Some</c>s.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AllSome() => maybes.All(m => m.IsSome);
    }
}
