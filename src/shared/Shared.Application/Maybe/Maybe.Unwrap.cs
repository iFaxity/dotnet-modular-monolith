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
        /// Returns the value if the maybe is Some, otherwise throws an exception.
        /// </summary>
        /// <returns>The contained value.</returns>
        /// <exception cref="MaybeNoneException"> when <see cref="IsNone"/> is <c>true</c>.</exception>
        public T Unwrap()
        {
            return maybe switch
            {
                ISome<T> some => some.Value,
                INone => throw new MaybeNoneException(),
                _ => throw new InvalidOperationException(),
            };
        }
    }
}
