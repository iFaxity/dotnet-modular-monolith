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
        /// Matches maybes via the two functors.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<TResult> MatchEach<TResult>(Func<T, TResult> mapSome, TResult none)
            where TResult : notnull => maybes.Select(m => m.Match(mapSome, none));
    }
}
