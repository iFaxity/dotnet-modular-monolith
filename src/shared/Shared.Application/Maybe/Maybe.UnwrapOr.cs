using System.Diagnostics.CodeAnalysis;
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
        /// <summary>
        /// Gets the value or returns the provided default value if <c>None</c>.
        /// </summary>
        /// <typeparam name="T">The type of the maybe value.</typeparam>
        /// <param name="maybe">The maybe instance.</param>
        /// <param name="defaultValue">The default value to return if value is not present.</param>
        /// <returns>The maybe value or the default value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull(nameof(defaultValue))]
        public T? UnwrapOr(T? defaultValue) => maybe.Match((value) => value, defaultValue);
    }
}
