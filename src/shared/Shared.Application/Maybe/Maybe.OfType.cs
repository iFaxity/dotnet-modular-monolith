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
        /// Attempts to downcast the value to a new type. Returns <c>None</c> if the cast fails.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <typeparam name="TResult">The target type to cast to.</typeparam>
        /// <returns>A maybe of the downcasted value or <c>None</c>.</returns>
        public IMaybe<TResult> OfType<TResult>()
            where TResult : class
        {
            if (!typeof(T).IsAssignableFrom(typeof(TResult)))
                return Maybe<TResult>.None;

            return maybe.Match((value) => Maybe.From(value as TResult), Maybe<TResult>.None);
        }
    }
}
