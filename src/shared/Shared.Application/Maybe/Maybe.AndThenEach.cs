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
        /// Bind values via the functor.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IMaybe<TValue>> AndThenEach<TValue>(Func<T, IMaybe<TValue>> functor)
            where TValue : notnull => maybes.Select(m => m.AndThen(functor));
    }
}
